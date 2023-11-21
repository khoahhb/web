using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Helpers.GenerateJWT;
using Web.Application.Helpers.GeneratePassword;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.CredentialRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;
using Web.Model.Dtos.User.Request;
using Web.Model.Dtos.User.Response;

namespace Web.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ICredentialRepository _credentialRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public UserService(IMapper mapper, IUserRepository userRepository, ICredentialRepository credentialRepository, 
            IUnitOfWork unitOfWork, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _credentialRepository = credentialRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResult<string>> SignInUser(SignInRequestDto signInRequestDto)
        {
            var user = await _userRepository.GetUserByUsername(signInRequestDto.Username, u => u.UserProfile);

            if (PasswordHandler.VerifyPassword(user.Password, signInRequestDto.Password))
            {
                var token = TokenHandler.CreateToken(_mapper.Map<UserResponseDto>(user), _configuration);
                await _credentialRepository.Create(new Credential() {Token= token, UserId = user.Id });
                await _unitOfWork.CommitAsync();
                return Success(token);
            }

            return Failure<string>(HttpStatusCode.Unauthorized);
        }

        public async Task<ServiceResult<UserResponseDto>> SignUpUser(CreateUserRequestDto signUpRequestDto)
        {
            User user = _mapper.Map<User>(signUpRequestDto);
            await _userRepository.Create(user);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserResponseDto>(user);
            await SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<string>> LogoutUser()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var credential = await _credentialRepository.GetOne(cre => cre.Token == token, cre => cre.User);
            if (credential == null)
                return Failure<string>(HttpStatusCode.NotFound);

            _credentialRepository.Delete(credential);
            await _unitOfWork.CommitAsync();

            return Success<string>(credential.User.Fullname);
        }

        public async Task<ServiceResult<UserResponseDto>> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = (await _userRepository.GetOneById(updateUserRequestDTO.Id, u => u.Avatar, u => u.UserProfile));
            _mapper.Map(updateUserRequestDTO, user);
            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<UserResponseDto>(user);
            await SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);

        }

        public async Task<ServiceResult<string>> DeleteUserById(Guid id)
        {
            var user = (await _userRepository.GetOneById(id));
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            var userId = user.Id;
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.Delete(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserById(Guid id)
        {
            var user = (await _userRepository.GetOneById(id, u => u.UserProfile, u => u.Avatar));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserResponseDto>(user);
            await SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByUsername(string username)
        {
            var user = (await _userRepository.GetUserByEmail(username));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserResponseDto>(user);
            await SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = (await _userRepository.GetUserByEmail(email));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<UserResponseDto>(user);
            await SetFullNamesForUserAndRelatedEntities(response);
            return Success(response);
        }

        public async Task<ServiceResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAll(u => u.UserProfile, u => u.Avatar);
            var response = users.Select(u => _mapper.Map<UserResponseDto>(u)).ToList();
            foreach (var user in response)
                await SetFullNamesForUserAndRelatedEntities(user);
            return Success(response.AsEnumerable());

        }

        private async Task SetFullNamesForUserAndRelatedEntities(UserResponseDto userResponse)
        {
            (userResponse.CreatedBy, userResponse.UpdatedBy) = await GetUserFullnames(Guid.Parse(userResponse.CreatedBy), Guid.Parse(userResponse.UpdatedBy));

            if (userResponse.Avatar != null)
            {
                (userResponse.Avatar.CreatedBy, userResponse.Avatar.UpdatedBy) = await GetUserFullnames(Guid.Parse(userResponse.Avatar.CreatedBy), Guid.Parse(userResponse.Avatar.UpdatedBy));
            }
            if (userResponse.UserProfile != null)
            {
                (userResponse.UserProfile.CreatedBy, userResponse.UserProfile.UpdatedBy) = await GetUserFullnames(Guid.Parse(userResponse.UserProfile.CreatedBy), Guid.Parse(userResponse.UserProfile.UpdatedBy));
            }
        }

        public async Task<(string CreatedFullName, string UpdatedFullName)> GetUserFullnames(Guid createdBy, Guid updatedBy)
        {
            var createdUser = await _userRepository.GetOneById(createdBy);
            var updatedUser = await _userRepository.GetOneById(updatedBy);
            return (createdUser.Fullname, updatedUser.Fullname);
        }
    }
}
