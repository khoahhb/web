using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Interfaces;
using Web.Model.Dtos.UserProfile.Request;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("v1/api/profile")]
    public class UserProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public UserProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        /// <summary>
        /// Create profile                     (Admin)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProfile(CreateUserProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.CreateProfile(dto);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Update Profile                     (Admin, Teacher)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher")]
        [HttpPut]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _profileService.UpdateProfile(dto);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Delete Profile                     (Admin) 
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileById(Guid id)
        {
            var result = await _profileService.DeleteProfileById(id);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, $"Deleted Profile {result.SuccessData}"),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "Profile not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Get Profile infor                     (Admin, Teacher, Student) 
        /// </summary>
        [Authorize(Roles = "Admin, Teacher, Student")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(Guid id)
        {
            var result = await _profileService.GetProfileById(id);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "Profile not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
        /// <summary>
        /// Get all Profile                     (Admin)
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("get_all")]
        public async Task<IActionResult> GetAllProfile()
        {
            var result = await _profileService.GetAllProfile();
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
    }
}
