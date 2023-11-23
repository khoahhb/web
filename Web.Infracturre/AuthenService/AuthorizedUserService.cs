using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Web.Model.EnumerationTypes;

namespace Web.Infracturre.AuthenService
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizedUserService( IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetUserId()
        {
            return IsValid() ? Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value) : null;
        }

        public string GetUserFullname()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

        public ProfileType? GetUserRole()
        {
            var roleString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            return roleString != null ? (ProfileType)Enum.Parse(typeof(ProfileType), roleString) : null;
        }

        public string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                return null;
            }

            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }

        public bool IsValid()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            return !string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ");
        }
    }
}
