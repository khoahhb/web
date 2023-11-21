using AutoMapper;
using System.Net;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.UserProfileRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;
using Web.Model.Dtos.UserProfile.Request;
using Web.Model.Dtos.UserProfile.Response;

namespace Web.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public ProfileService(IMapper mapper, IUserRepository userRepository, IUserProfileRepository userProfileRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<UserProfileResponseDto>> CreateProfile(CreateUserProfileRequestDto dto)
        {
            var profile = _mapper.Map<UserProfile>(dto);
            await _userProfileRepository.Create(profile);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<UserProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto)
        {
            var profile = (await _userProfileRepository.GetOneById(dto.Id));
            _mapper.Map(dto, profile);
            _userProfileRepository.Update(profile);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<string>> DeleteProfileById(Guid id)
        {
            var profile = (await _userProfileRepository.GetOneById(id));
            if (profile == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userProfileRepository.Delete(profile);
            await _unitOfWork.CommitAsync();
            return Success(profile.Id.ToString());
        }

        public async Task<ServiceResult<UserProfileResponseDto>> GetProfileById(Guid id)
        {
            var profile = (await _userProfileRepository.GetOneById(id));
            if (profile == null)
                return Failure<UserProfileResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<IEnumerable<UserProfileResponseDto>>> GetAllProfile()
        {
            var profiles = await _userProfileRepository.GetAll();
            var responses = profiles.Select(p => _mapper.Map<UserProfileResponseDto>(p)).ToList();
            foreach (var response in responses)
            {
                await SetFullNamesForUserAndRelatedEntities(response);
            }
            return Success(responses.AsEnumerable());
        }

        private async Task SetFullNamesForUserAndRelatedEntities(UserProfileResponseDto userResponse)
        {
            (userResponse.CreatedBy, userResponse.UpdatedBy) = await GetUserFullnames(Guid.Parse(userResponse.CreatedBy), Guid.Parse(userResponse.UpdatedBy));
        }

        public async Task<(string CreatedFullName, string UpdatedFullName)> GetUserFullnames(Guid createdBy, Guid updatedBy)
        {
            var createdUser = await _userRepository.GetOneById(createdBy);
            var updatedUser = await _userRepository.GetOneById(updatedBy);

            return (createdUser.Fullname, updatedUser.Fullname);
        }
    }
}
