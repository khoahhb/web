namespace Web.Model.Dtos.ResponseDtos
{
    public class AvatarResponseDto
    {
        public Guid Id { get; set; }
        public string? FileName { get; set; }
        public string? MimeType { get; set; }
        public long? FileSize { get; set; }
        public bool? IsPublished { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
