using Microsoft.AspNetCore.Http;

namespace Web.Model.Dtos.Avatar.Request
{
    public class UpdateAvatarRequestDto
    {
        public Guid Id { get; set; }
        public IFormFile? File { get; set; }
        public bool? IsPublished { get; set; }
    }
}
