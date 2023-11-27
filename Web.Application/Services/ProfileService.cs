using AutoMapper;
using System.Net;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Helpers.DateTimeHandlers;
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
            _userProfileRepository.Add(profile);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<UserProfileResponseDto>> UpdateProfile(UpdateUserProfileRequestDto dto)
        {
            var profile = _userProfileRepository.GetOneById(dto.Id);
            _mapper.Map(dto, profile);
            _userProfileRepository.Update(profile);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<string>> DeleteProfileById(Guid id)
        {
            var profile =  _userProfileRepository.GetOneById(id);
            if (profile == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userProfileRepository.Delete(profile);
            await _unitOfWork.CommitAsync();
            return Success(profile.Id.ToString());
        }

        public ServiceResult<UserProfileResponseDto> GetProfileById(Guid id)
        {
            var profile =  _userProfileRepository.GetOneById(id);
            if (profile == null)
                return Failure<UserProfileResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserProfileResponseDto>(profile);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public  ServiceResult<List<UserProfileResponseDto>> GetAllProfile()
        {
            var profiles = _userProfileRepository.GetAll();


            List<UserProfileResponseDto> response = profiles.ToList()
                                .Select(u =>
                                {
                                    var profile = new UserProfileResponseDto() 
                                    { 
                                        Id = u.Id,
                                        Name = u.Name,
                                        Type = u.Type,
                                        Descrtiption = u.Descrtiption,
                                        CreatedAt = u.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        UpdatedAt = u.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        CreatedBy = u.CreatedBy.ToString(),
                                        UpdatedBy = u.UpdatedBy.ToString(),
                                    };

                                    SetFullNamesForUserAndRelatedEntities(profile);
                                    return profile;
                                }).ToList();

            return Success(response);
        }

        private void SetFullNamesForUserAndRelatedEntities(UserProfileResponseDto re)
        {
            re.CreatedBy = GetUserFullname(Guid.Parse(re.CreatedBy));
            re.UpdatedBy = GetUserFullname(Guid.Parse(re.UpdatedBy));
        }

        public string GetUserFullname(Guid id)
        {
            var user = _userRepository.GetOneById(id);
            return user.Fullname;
        }
    }
}
