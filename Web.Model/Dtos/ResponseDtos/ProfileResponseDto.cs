using Web.Model.Enum;

namespace Web.Model.Dtos.ResponseDtos
{
    public class ProfileResponseDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
