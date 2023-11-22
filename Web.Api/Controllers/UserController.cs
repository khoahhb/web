using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Interfaces;
using Web.Model.Dtos.User.Request;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("v1/api/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create user                     (Admin)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserRequestDto signUpDto)
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
        /// Update user                     (Admin, Teacher)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateUser(updateUserRequestDTO);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Delete user                     (Admin) 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var result = await _userService.DeleteUserById(id);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, $"Deleted user {result.SuccessData}"),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "User not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Get user infor                     (Admin, Teacher, Student)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher, Student")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserById(id);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "User not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Get all user                     (Admin, Teacher)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
    }
}
