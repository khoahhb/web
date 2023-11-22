using Microsoft.AspNetCore.Http;

namespace Web.Model.Dtos.Avatar.Request
{
    public class CreateAvatarRequestDto
    {
        public IFormFile? File { get; set; }
        public bool? IsPublished { get; set; }
    }
}
