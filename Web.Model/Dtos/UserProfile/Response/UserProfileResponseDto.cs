using Web.Model.Dtos.Base;
using Web.Model.EnumerationTypes;

namespace Web.Model.Dtos.UserProfile.Response
{
    public class UserProfileResponseDto : BaseResponseDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public ProfileType Type { get; set; }
        public string? Descrtiption { get; set; }
    }
}
