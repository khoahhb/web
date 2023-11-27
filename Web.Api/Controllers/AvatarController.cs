using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.Application.Interfaces;
using Web.Model.Dtos.Avatar.Request;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("v1/api/avatar")]
    public class AvatarController : Controller
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        /// <summary>
        /// Create (upload) avatar                     (Admin, Teacher, Student)
        /// </summary>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadAvatar([FromForm] CreateAvatarRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _avatarService.CreateAvatar(dto);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Update avatar                     (Admin, Teacher, Student)
        /// </summary>
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _avatarService.UpdateAvatar(dto);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Delete avatar                     (Admin, Teacher)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAvatarById(Guid id)
        {
            var result = await _avatarService.DeleteAvatarById(id);

            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, $"Deleted avatar {result.SuccessData}"),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "Avatar not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }

        /// <summary>
        /// Get avatar infor                     (Admin, Teacher, Student)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher, Student")]
        [HttpGet("{id}")]
        public  IActionResult GetAvatarById(Guid id)
        {
            var result = _avatarService.GetAvatarById(id);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                HttpStatusCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, "Avatar not found."),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
        /// <summary>
        /// Get all avatar                     (Admin, Teacher)
        /// </summary>
        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet("get_all")]
        public IActionResult GetAllAvatar()
        {
            var result =  _avatarService.GetAllAvatar();
            return result.StatusCode switch
            {
                HttpStatusCode.OK => StatusCode((int)HttpStatusCode.OK, result.SuccessData),
                _ => StatusCode((int)HttpStatusCode.ServiceUnavailable, "Service Unavailable.")
            };
        }
    }
}
