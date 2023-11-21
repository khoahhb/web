using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Net;
using Web.Application.Helpers;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Interfaces;
using Web.Domain.Entities;
using Web.Infracturre.Repositories.AvatarRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Infracturre.UnitOfWorks;
using Web.Model.Dtos.Avatar.Request;
using Web.Model.Dtos.Avatar.Response;

namespace Web.Application.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IAvatarRepository _avatarRepository;
        private readonly IUnitOfWork _unitOfWork;

        private ServiceResult<T> Success<T>(T data) => new ServiceResult<T>().SuccessResult(HttpStatusCode.OK, data);
        private ServiceResult<T> Failure<T>(HttpStatusCode statusCode) => new ServiceResult<T>().Failure(statusCode);

        public AvatarService(IMapper mapper, IUserRepository userRepository, IAvatarRepository avatarRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _avatarRepository = avatarRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<AvatarResponseDto>> CreateAvatar(CreateAvatarRequestDto dto)
        {
            PhotoSettings.SaveAvatarToUploads(dto.File);
            var avatar = _mapper.Map<Avatar>(dto);
            await _avatarRepository.Create(avatar);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<AvatarResponseDto>> UpdateAvatar(UpdateAvatarRequestDto dto)
        {
            var avatar = (await _avatarRepository.GetOneById(dto.Id));
            _mapper.Map(dto, avatar);
            _avatarRepository.Update(avatar);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<string>> DeleteAvatarById(Guid id)
        {
            var avatar = (await _avatarRepository.GetOneById(id));
            if (avatar == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _avatarRepository.Delete(avatar);
            await _unitOfWork.CommitAsync();
            return Success(avatar.Id.ToString());
        }

        public async Task<ServiceResult<AvatarResponseDto>> GetAvatarById(Guid id)
        {
            var avatar = (await _avatarRepository.GetOneById(id));
            if (avatar == null)
                return Failure<AvatarResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            await SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<IEnumerable<AvatarResponseDto>>> GetAllAvatar()
        {
            var avatars = await _avatarRepository.GetAll();
            var response = avatars.Select(a => _mapper.Map<AvatarResponseDto>(a)).ToList();
            foreach (var user in response)
            {
                await SetFullNamesForUserAndRelatedEntities(user);
            }
            return Success(response.AsEnumerable());
        }

        private async Task SetFullNamesForUserAndRelatedEntities(AvatarResponseDto userResponse)
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
