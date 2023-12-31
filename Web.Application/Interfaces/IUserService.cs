﻿using Web.Application.Helpers.APIResponseCustom;
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
        public Task<ServiceResult<string>> DeleteUserByUsername(string username);
        public Task<ServiceResult<string>> DeleteUserByEmail(string email);
        public Task<ServiceResult<UserResponseDto>> GetUserById(Guid id);
        public Task<ServiceResult<UserResponseDto>> GetUserByUsername(string username);
        public Task<ServiceResult<UserResponseDto>> GetUserByEmail(string email);
        public Task<ServiceResult<IEnumerable<UserResponseDto>>> GetAllUsers();
    }
}
