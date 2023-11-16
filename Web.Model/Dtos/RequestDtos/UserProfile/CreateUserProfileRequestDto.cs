using Web.Model.Enum;

namespace Web.Model.Dtos.RequestDtos.UserProfile
{
    public class CreateUserProfileRequestDto
    {
        public string? Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
    }
}
