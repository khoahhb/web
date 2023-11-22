using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.AvatarRepo;
using Web.Infracturre.Repositories.UserProfileRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;
using Web.Model.EnumerationTypes;

namespace Web.Application.Services
{
    public class AuthorizedUserService : IAuthorizedUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizedUserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        public User GetUser()
        {
            var id = GetUserId();

            return _userRepository.GetOneById(id);
        }

        public string GetUserFullname()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        }

        public ProfileType GetUserRole()
        {
            var roleString = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            ProfileType roleType = (ProfileType)Enum.Parse(typeof(ProfileType), roleString);
            return roleType;
        }

        public string GetToken()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            return authorizationHeader.Substring("Bearer ".Length).Trim();
        }
    }
}
