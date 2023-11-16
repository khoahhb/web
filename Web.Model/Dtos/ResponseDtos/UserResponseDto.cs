namespace Web.Model.Dtos.ResponseDtos
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public AvatarResponseDto? Avatar { get; set; }
        public ProfileResponseDto? UserProfile { get; set; }
    }
}
