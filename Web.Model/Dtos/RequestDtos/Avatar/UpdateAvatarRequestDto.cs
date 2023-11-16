using Microsoft.AspNetCore.Http;

namespace Web.Model.Dtos.RequestDtos.Avatar
{
    public class UpdateAvatarRequestDto
    {
        public Guid Id { get; set; }
        public IFormFile? File { get; set; }
        public bool? IsPublished { get; set; }
    }
}
