using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using Web.Application.Helpers;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;
using Web.Infracturre.Repositories;
using Web.Model.Dtos.RequestDtos.Avatar;
using Web.Model.Dtos.ResponseDtos;

namespace Web.Application.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IMapper _mapper;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public AvatarService(IMapper mapper, IAvatarRepository avatarRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _mapper = mapper;
            _avatarRepository = avatarRepository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task<ServiceResult<AvatarResponseDto>> CreateAvatar(CreateAvatarRequestDto dto)
        {
            PhotoSettings.SaveAvatarToUploads(dto.File);
            var avatar = _mapper.Map<Avatar>(dto);
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                avatar.CreatedBy = currentUserName;
                avatar.UpdatedBy = currentUserName;
            }
            await _avatarRepository.CreateAvatar(avatar);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<AvatarResponseDto>> UpdateAvatar(UpdateAvatarRequestDto dto)
        {
            var avatar = (await _avatarRepository.GetAvatarById(dto.Id));
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                avatar.UpdatedBy = currentUserName;
            }
            _mapper.Map(dto, avatar);
            _avatarRepository.UpdateAvatar(avatar);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<string>> DeleteAvatarById(Guid id)
        {
            var avatar = (await _avatarRepository.GetAvatarById(id));
            if (avatar == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _avatarRepository.DeleteAvatar(avatar);
            await _unitOfWork.CommitAsync();
            return Success(avatar.Id.ToString());
        }

        public async Task<ServiceResult<AvatarResponseDto>> GetAvatarById(Guid id)
        {
            var avatar = (await _avatarRepository.GetAvatarById(id));
            if (avatar == null)
                return Failure<AvatarResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<IEnumerable<AvatarResponseDto>>> GetAllAvatar()
        {
            var avatars = await _avatarRepository.GetAllAvatars();
            var response = avatars.Select(_mapper.Map<AvatarResponseDto>);
            return Success(response);
        }
    }
}
