using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Interfaces;
using Web.Application.Services;
using Web.Model.Dtos.User.Request;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("v1/api/auth")]

    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Sign Out    (All)
        /// </summary>
        [Authorize]
        [HttpPost("signout")]
        public async Task<IActionResult> SignOutUser()
        {
            var result = await _userService.SignOutUser();
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, $"You ({result.SuccessData}) is logged out."),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "Your token is not valid (Not found in credential list)."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Sign In user
        /// </summary>
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto signInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.SignInUser(signInDto);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                HttpStatusCode.Unauthorized => StatusCode((int)HttpStatusCode.Unauthorized, "Your password do not match."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Sign Up user
        /// </summary>
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(CreateUserRequestDto signUpDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.SignUpUser(signUpDto);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Get current user info (All)
        /// </summary>
        [Authorize]
        [HttpPost]
        public IActionResult GetCurrentUserInfo()
        {
            var result = _userService.GetCurrentUserInfo();
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "User not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
    }
}
