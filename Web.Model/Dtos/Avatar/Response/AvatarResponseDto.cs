using Web.Model.Dtos.Base;

namespace Web.Model.Dtos.Avatar.Response
{
    public class AvatarResponseDto : BaseResponseDTO
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? MimeType { get; set; }
        public long? FileSize { get; set; }
        public bool? IsPublished { get; set; }
    }
}
