using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using Web.Application.Helpers;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Interfaces;
using Web.Model.Dtos.RequestDtos.UserProfile;
using Web.Model.Dtos.ResponseDtos;

namespace Web.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public ProfileService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResult<ProfileResponseDto>> CreateProfile(CreateUserProfileRequestDto dto)
        {
            var profile = _mapper.Map<UserProfile>(dto);
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                profile.CreatedBy = currentUserName;
                profile.UpdatedBy = currentUserName;
            }
            await _unitOfWork.RepositoryProfile.Insert(profile);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<ProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto)
        {
            var profile = (await _unitOfWork.RepositoryProfile.GetSingleById(dto.Id));
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                profile.UpdatedBy = currentUserName;
            }
            _mapper.Map(dto, profile);
            _unitOfWork.RepositoryProfile.Update(profile);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<string>> DeleteProfileById(Guid id)
        {
            var profile = (await _unitOfWork.RepositoryProfile.GetSingleById(id));
            if (profile == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _unitOfWork.RepositoryProfile.Delete(profile);
            await _unitOfWork.Commit();
            return Success(profile.Id.ToString());
        }

        public async Task<ServiceResult<ProfileResponseDto>> GetProfileById(Guid id)
        {
            var profile = (await _unitOfWork.RepositoryProfile.GetSingleById(id));
            if (profile == null)
                return Failure<ProfileResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<IEnumerable<ProfileResponseDto>>> GetAllProfile()
        {
            var response = (await _unitOfWork.RepositoryProfile.GetAll()).Select(_mapper.Map<ProfileResponseDto>);
            return Success(response);
        }
    }
}
