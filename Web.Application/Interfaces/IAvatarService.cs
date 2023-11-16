using Web.Application.Helpers;
using Web.Model.Dtos.RequestDtos.Avatar;
using Web.Model.Dtos.ResponseDtos;

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
