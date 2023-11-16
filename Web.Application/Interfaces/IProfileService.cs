using Web.Application.Helpers;
using Web.Model.Dtos.RequestDtos.UserProfile;
using Web.Model.Dtos.ResponseDtos;

namespace Web.Application.Interfaces
{
    public interface IProfileService
    {
        public Task<ServiceResult<ProfileResponseDto>> CreateProfile(CreateUserProfileRequestDto dto);
        public Task<ServiceResult<ProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto);
        public Task<ServiceResult<string>> DeleteProfileById(Guid id);
        public Task<ServiceResult<ProfileResponseDto>> GetProfileById(Guid id);
        public Task<ServiceResult<IEnumerable<ProfileResponseDto>>> GetAllProfile();
    }
}
