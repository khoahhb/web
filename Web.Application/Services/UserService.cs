using AutoMapper;
using LinqKit;
using Microsoft.Extensions.Configuration;
using System.Net;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Helpers.DateTimeHandlers;
using Web.Application.Helpers.GenerateJWT;
using Web.Application.Helpers.GeneratePassword;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.AuthenService;
using Web.Infracturre.Repositories.CredentialRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;
using Web.Model.Dtos.Avatar.Response;
using Web.Model.Dtos.User.Request;
using Web.Model.Dtos.User.Response;
using Web.Model.Dtos.UserProfile.Response;
using Web.Model.EnumerationTypes;

namespace Web.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICredentialRepository _credentialRepository;
        private readonly IAuthorizedUserService _authorizedUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public UserService(IMapper mapper, IUserRepository userRepository, ICredentialRepository credentialRepository, IAuthorizedUserService authorizedUserService,
            IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _credentialRepository = credentialRepository;
            _authorizedUserService = authorizedUserService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ServiceResult<string>> SignInUser(SignInRequestDto signInRequestDto)
        {
            var user = _userRepository.GetByUsername(signInRequestDto.Username, u => u.UserProfile);

            if (PasswordHandler.VerifyPassword(user.Password, signInRequestDto.Password))
            {
                var token = TokenHandler.CreateToken(_mapper.Map<UserResponseDto>(user), _configuration);
                _credentialRepository.Add(new Credential() { Token = token, UserId = user.Id });
                await _unitOfWork.CommitAsync();
                return Success(token);
            }

            return Failure<string>(HttpStatusCode.Unauthorized);
        }

        public async Task<ServiceResult<UserResponseDto>> SignUpUser(CreateUserRequestDto signUpRequestDto)
        {
            User user = _mapper.Map<User>(signUpRequestDto);
            _userRepository.Add(user);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserResponseDto>(user);
            SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<string>> LogoutUser()
        {
            var token = _authorizedUserService.GetToken();
            var credential = _credentialRepository.GetOne(cre => cre.Token == token, cre => cre.User);
            if (credential == null)
                return Failure<string>(HttpStatusCode.NotFound);

            _credentialRepository.Delete(credential);
            await _unitOfWork.CommitAsync();
            return Success<string>(credential.User.Fullname);
        }

        public async Task<ServiceResult<UserResponseDto>> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = _userRepository.GetOneById(updateUserRequestDTO.Id, u => u.Avatar, u => u.UserProfile);
            _mapper.Map(updateUserRequestDTO, user);
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserResponseDto>(user);
            SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<string>> DeleteUserById(Guid id)
        {
            var user = _userRepository.GetOneById(id);
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByEmail(string email)
        {
            var user = _userRepository.GetByEmail(email);
            var userId = user.Id;
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public ServiceResult<UserResponseDto> GetUserById(Guid id)
        {
            var user = _userRepository.GetOneById(id, u => u.UserProfile, u => u.Avatar);
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserResponseDto>(user);
            SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        private void SetFullNamesForUserAndRelatedEntities(UserResponseDto re)
        {
            re.CreatedBy = GetUserFullname(Guid.Parse(re.CreatedBy));
            re.UpdatedBy = GetUserFullname(Guid.Parse(re.UpdatedBy));
            if(re.Avatar != null)
            {
                re.Avatar.CreatedBy = GetUserFullname(Guid.Parse(re.Avatar.CreatedBy));
                re.Avatar.UpdatedBy = GetUserFullname(Guid.Parse(re.Avatar.UpdatedBy));
            }  
            
            if(re.UserProfile != null)
            {
                re.UserProfile.CreatedBy = GetUserFullname(Guid.Parse(re.UserProfile.CreatedBy));
                re.UserProfile.UpdatedBy = GetUserFullname(Guid.Parse(re.UserProfile.UpdatedBy));
            }

        }

        public string GetUserFullname(Guid id)
        {
            var user = _userRepository.GetOneById(id);
            return user.Fullname;
        }

        public ServiceResult<List<UserResponseDto>> GetAllUsers()
        {
            var current_UserId = _authorizedUserService.GetUserId();
            var current_Role = _authorizedUserService.GetUserRole();

            ExpressionStarter<User> predicate = PredicateBuilder.New<User>();
            predicate.Or(u => u.Id == current_UserId);
            if(current_Role == ProfileType.Teacher)
            {
                predicate.Or(u => u.UserProfile.Type == ProfileType.Student);
                predicate.Or(u => u.UserProfile.Type == ProfileType.None);
            }
            var userList = _userRepository.GetAll(u => u.UserProfile, u => u.Avatar).Where(predicate).ToList();

            List<UserResponseDto> response = userList
                                .Select(u =>
                                {
                                    var user = new UserResponseDto()
                                    {
                                        Id = u.Id,
                                        Username = u.Username,
                                        Fullname = u.Fullname,
                                        Gender = u.Gender.ToString(),
                                        DateOfBirth = u.DateOfBirth.HasValue ? u.DateOfBirth.Value
                                        .ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy") : null,
                                        Phone = u.Phone,
                                        Email = u.Email,
                                        Address = u.Address,
                                        Avatar = _mapper.Map<AvatarResponseDto>(u.Avatar),
                                        UserProfile = _mapper.Map<UserProfileResponseDto>(u.UserProfile),
                                        CreatedAt = u.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        UpdatedAt = u.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        CreatedBy = u.CreatedBy.ToString(),
                                        UpdatedBy = u.UpdatedBy.ToString(),
                                    };

                                    SetFullNamesForUserAndRelatedEntities(user);
                                    return user;
                                }).ToList();

            return Success(response);
        }
    }
}
