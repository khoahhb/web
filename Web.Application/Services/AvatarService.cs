using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using Web.Application.Helpers;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;
using Web.Model.Dtos.RequestDtos.Avatar;
using Web.Model.Dtos.ResponseDtos;

namespace Web.Application.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public AvatarService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
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
            await _unitOfWork.RepositoryAvatar.Insert(avatar);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<AvatarResponseDto>> UpdateAvatar(UpdateAvatarRequestDto dto)
        {
            var avatar = (await _unitOfWork.RepositoryAvatar.GetSingleById(dto.Id));
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                avatar.UpdatedBy = currentUserName;
            }
            _mapper.Map(dto, avatar);
            _unitOfWork.RepositoryAvatar.Update(avatar);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<string>> DeleteAvatarById(Guid id)
        {
            var avatar = (await _unitOfWork.RepositoryAvatar.GetSingleById(id));
            if (avatar == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _unitOfWork.RepositoryAvatar.Delete(avatar);
            await _unitOfWork.Commit();
            return Success(avatar.Id.ToString());
        }

        public async Task<ServiceResult<AvatarResponseDto>> GetAvatarById(Guid id)
        {
            var avatar = (await _unitOfWork.RepositoryAvatar.GetSingleById(id));
            if (avatar == null)
                return Failure<AvatarResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<AvatarResponseDto>(avatar));
        }

        public async Task<ServiceResult<IEnumerable<AvatarResponseDto>>> GetAllAvatar()
        {
            var response = (await _unitOfWork.RepositoryAvatar.GetAll()).Select(_mapper.Map<AvatarResponseDto>);
            return Success(response);
        }
    }
}
