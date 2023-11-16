using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using Web.Application.Helpers;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Domain.Helpers;
using Web.Infracturre.Interfaces;
using Web.Model.Dtos.RequestDtos.User;
using Web.Model.Dtos.ResponseDtos;

namespace Web.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public UserService(IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork,  IConfiguration configuration)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ServiceResult<string>> SignInUser(SignInRequestDto signInRequestDto)
        {
            var user = await _userRepository.GetUserByUsername( signInRequestDto.Username,u => u.UserProfile );

            if (PasswordHandler.VerifyPassword(user.Password, signInRequestDto.Password))
                return Success(TokenHandler.CreateToken(_mapper.Map<UserResponseDto>(user), 60, _configuration));

            return Failure<string>(HttpStatusCode.Unauthorized);
        }

        public async Task<ServiceResult<UserResponseDto>> SignUpUser(CreateUserRequestDto signUpRequestDto)
        {
            User user = _mapper.Map<User>(signUpRequestDto);
            await _userRepository.CreateUser(user);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = (await _userRepository.GetUserById(updateUserRequestDTO.Id, u => u.Avatar, u => u.UserProfile));
            _mapper.Map(updateUserRequestDTO, user);
            _userRepository.UpdateUser(user);
            await _unitOfWork.CommitAsync();
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<string>> DeleteUserById(Guid id)
        {
            var user = (await _userRepository.GetUserById(id));
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.DeleteUser(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByUsername(string username)
        {
            var user = await _userRepository.GetUserByUsername(username);
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.DeleteUser(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            var userId = user.Id;
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _userRepository.DeleteUser(user);
            await _unitOfWork.CommitAsync();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserById(Guid id)
        {
            var user = (await _userRepository.GetUserById(id,  u => u.UserProfile, u => u.Avatar ));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByUsername(string username)
        {
            var user = (await _userRepository.GetUserByEmail( username));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = (await _userRepository.GetUserByEmail(email));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers(u => u.UserProfile, u => u.Avatar);
            var response = users.Select(_mapper.Map<UserResponseDto>);
            return Success(response);
        }
    }
}
