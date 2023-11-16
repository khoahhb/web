using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Interfaces;
using Web.Model.Dtos.RequestDtos.User;

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
        /// SignUp user
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
        /// SignIn user
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
                HttpStatusCode.Unauthorized => StatusCode((int)HttpStatusCode.Unauthorized, "Password do not match."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
    }
}
