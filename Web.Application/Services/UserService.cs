using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ServiceResult<string>> SignInUser(SignInRequestDto signInRequestDto)
        {
            var user = await _unitOfWork.RepositoryUser.GetSingleByCondition(
                user => user.Username == signInRequestDto.Username, 
                new Expression<Func<User, object>>[] { u => u.UserProfile });

            if (PasswordHandler.VerifyPassword(user.Password, signInRequestDto.Password))
                return Success(TokenHandler.CreateToken(_mapper.Map<UserResponseDto>(user), 60, _configuration));
            
            return Failure<string>(HttpStatusCode.Unauthorized);
        }

        public async Task<ServiceResult<UserResponseDto>> SignUpUser(CreateUserRequestDto signUpRequestDto)
        {
            User user = _mapper.Map<User>(signUpRequestDto);
            user.CreatedBy = user.Fullname;
            user.UpdatedBy = user.Fullname;
            await _unitOfWork.RepositoryUser.Insert(user);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO)
        {
            var user = (await _unitOfWork.RepositoryUser.GetSingleById(updateUserRequestDTO.Id));
            var currentUserName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrEmpty(currentUserName))
            {
                user.UpdatedBy = currentUserName;
            }
            _mapper.Map(updateUserRequestDTO, user);
            _unitOfWork.RepositoryUser.Update(user);
            await _unitOfWork.Commit();
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<string>> DeleteUserById(Guid id)
        {
            var user = (await _unitOfWork.RepositoryUser.GetSingleById(id));
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _unitOfWork.RepositoryUser.Delete(user);
            await _unitOfWork.Commit();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByUsername(string username)
        {
            var user = await _unitOfWork.RepositoryUser.GetSingleByCondition(u => u.Username == username);
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _unitOfWork.RepositoryUser.Delete(user);
            await _unitOfWork.Commit();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<string>> DeleteUserByEmail(string email)
        {
            var user = await _unitOfWork.RepositoryUser.GetSingleByCondition(u => u.Email == email);
            var userId = user.Id;
            if (user == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _unitOfWork.RepositoryUser.Delete(user);
            await _unitOfWork.Commit();
            return Success(user.Id.ToString());
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserById(Guid id)
        {
            var user = (await _unitOfWork.RepositoryUser.GetSingleById(id, new Expression<Func<User, object>>[] { u => u.UserProfile, u => u.Avatar }));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByUsername(string username)
        {
            var user = (await _unitOfWork.RepositoryUser.GetSingleByCondition(user => user.Username == username));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = (await _unitOfWork.RepositoryUser.GetSingleByCondition(user => user.Email == email));
            if (user == null)
                return Failure<UserResponseDto>(HttpStatusCode.NotFound);
            return Success(_mapper.Map<UserResponseDto>(user));
        }

        public async Task<ServiceResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var response = (await _unitOfWork.RepositoryUser
                            .GetAll(new Expression<Func<User, object>>[] { u => u.UserProfile, u => u.Avatar }))
                            .Select(_mapper.Map<UserResponseDto>);
            return Success(response);
        }
    }
}
