using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Web.Infracturre.Repositories.CredentialRepo;

namespace Web.Api.Middlewares
{
    public class TokenValidationHandler : JwtBearerHandler
    {
        private readonly ICredentialRepository _credentialRepository;

        public TokenValidationHandler(
            ICredentialRepository credentialRepository,
            IOptionsMonitor<JwtBearerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _credentialRepository = credentialRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader?.StartsWith("Bearer ") != true)
            {
                return AuthenticateResult.Fail("You are not logged in.");
            }

            var token = authorizationHeader["Bearer ".Length..].Trim();
            if (!await _credentialRepository.IsValidAsync(token))
            {
                return AuthenticateResult.Fail("Your token is not valid (Not found in credential list).");
            }

            return AuthenticateResult.Success(new AuthenticationTicket(GetClaims(token), Scheme.Name));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            Response.ContentType = "application/json";

            var fail = await HandleAuthenticateOnceSafeAsync();
            var payload = JsonConvert.SerializeObject(new { error = fail.Failure.Message });

            await Response.WriteAsync(payload);
        }

        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 403;
            Response.ContentType = "application/json";

            var payload = JsonConvert.SerializeObject(new { error = "Your role not valid for this route." });
            await Response.WriteAsync(payload);
        }

        private ClaimsPrincipal GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, Scheme.Name);
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
