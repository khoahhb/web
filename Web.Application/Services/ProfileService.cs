using AutoMapper;
using System.Net;
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
        private readonly IMapper _mapper;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public ProfileService(IMapper mapper, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<ProfileResponseDto>> CreateProfile(CreateUserProfileRequestDto dto)
        {
            var profile = _mapper.Map<UserProfile>(dto);
            await _userProfileRepository.CreateUserProfile(profile);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<ProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto)
        {
            var profile = (await _userProfileRepository.GetUserProfileById(dto.Id));
            _mapper.Map(dto, profile);
            _userProfileRepository.UpdateUserProfile(profile);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<string>> DeleteProfileById(Guid id)
        {
            var profile = (await _userProfileRepository.GetUserProfileById(id));
            if (profile == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userProfileRepository.DeleteUserProfile(profile);
            await _unitOfWork.CommitAsync();
            return Success(profile.Id.ToString());
        }

        public async Task<ServiceResult<ProfileResponseDto>> GetProfileById(Guid id)
        {
            var profile = (await _userProfileRepository.GetUserProfileById(id));
            if (profile == null)
                return Failure<ProfileResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<ProfileResponseDto>(profile));
        }

        public async Task<ServiceResult<IEnumerable<ProfileResponseDto>>> GetAllProfile()
        {
            var profiles = await _userProfileRepository.GetAllUserProfiles();
            var response = profiles.Select(_mapper.Map<ProfileResponseDto>);
            return Success(response);
        }
    }
}
