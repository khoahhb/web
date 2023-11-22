using Web.Model.Dtos.Avatar.Response;
using Web.Model.Dtos.Base;
using Web.Model.Dtos.UserProfile.Response;

namespace Web.Model.Dtos.User.Response
{
    public class UserResponseDto : BaseResponseDTO
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public AvatarResponseDto? Avatar { get; set; }
        public UserProfileResponseDto? UserProfile { get; set; }
    }
}
