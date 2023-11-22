using Web.Application.Helpers.APIResponseCustom;
using Web.Domain.Entities;
using Web.Model.Dtos.User.Request;
using Web.Model.Dtos.User.Response;

namespace Web.Application.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResult<string>> SignInUser(SignInRequestDto signInRequestDto);
        public Task<ServiceResult<UserResponseDto>> SignUpUser(CreateUserRequestDto signUpRequestDto);
        public Task<ServiceResult<string>> LogoutUser();
        public Task<ServiceResult<UserResponseDto>> UpdateUser(UpdateUserRequestDTO updateUserRequestDTO);
        public Task<ServiceResult<string>> DeleteUserById(Guid id);
        public ServiceResult<UserResponseDto> GetUserById(Guid id);
        public ServiceResult<List<UserResponseDto>> GetAllUsers();
    }
}
