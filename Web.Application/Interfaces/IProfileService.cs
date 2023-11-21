using Web.Application.Helpers.APIResponseCustom;
using Web.Model.Dtos.UserProfile.Request;
using Web.Model.Dtos.UserProfile.Response;

namespace Web.Application.Interfaces
{
    public interface IProfileService
    {
        public Task<ServiceResult<UserProfileResponseDto>> CreateProfile(CreateUserProfileRequestDto dto);
        public Task<ServiceResult<UserProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto);
        public Task<ServiceResult<string>> DeleteProfileById(Guid id);
        public Task<ServiceResult<UserProfileResponseDto>> GetProfileById(Guid id);
        public Task<ServiceResult<IEnumerable<UserProfileResponseDto>>> GetAllProfile();
    }
}
