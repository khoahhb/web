using Web.Model.EnumerationTypes;

namespace Web.Model.Dtos.UserProfile.Request
{
    public class CreateUserProfileRequestDto
    {
        public string? Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
    }
}
