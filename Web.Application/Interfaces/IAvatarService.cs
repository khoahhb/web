using Web.Application.Helpers.APIResponseCustom;
using Web.Model.Dtos.Avatar.Request;
using Web.Model.Dtos.Avatar.Response;

namespace Web.Application.Interfaces
{
    public interface IAvatarService
    {
        public Task<ServiceResult<AvatarResponseDto>> CreateAvatar(CreateAvatarRequestDto dto);
        public Task<ServiceResult<AvatarResponseDto>> UpdateAvatar(UpdateAvatarRequestDto dto);
        public Task<ServiceResult<string>> DeleteAvatarById(Guid id);
        public Task<ServiceResult<AvatarResponseDto>> GetAvatarById(Guid id);
        public Task<ServiceResult<IEnumerable<AvatarResponseDto>>> GetAllAvatar();
    }
}
