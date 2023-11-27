using AutoMapper;
using System.Net;
using Web.Application.Helpers;
using Web.Application.Helpers.APIResponseCustom;
using Web.Application.Helpers.DateTimeHandlers;
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
            await PhotoSettings.SaveAvatarToUploads(dto.File);
            var avatar = _mapper.Map<Avatar>(dto);
            _avatarRepository.Add(avatar);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<AvatarResponseDto>> UpdateAvatar(UpdateAvatarRequestDto dto)
        {
            var avatar =  _avatarRepository.GetOneById(dto.Id);
            _mapper.Map(dto, avatar);
            _avatarRepository.Update(avatar);
            await _unitOfWork.CommitAsync();
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public async Task<ServiceResult<string>> DeleteAvatarById(Guid id)
        {
            var avatar =  _avatarRepository.GetOneById(id);
            if (avatar == null)
                return Failure<string>(HttpStatusCode.NotFound);
            _avatarRepository.Delete(avatar);
            await _unitOfWork.CommitAsync();
            return Success(avatar.Id.ToString());
        }

        public ServiceResult<AvatarResponseDto> GetAvatarById(Guid id)
        {
            var avatar =  _avatarRepository.GetOneById(id);
            if (avatar == null)
                return Failure<AvatarResponseDto>(HttpStatusCode.NotFound);
            var response = _mapper.Map<AvatarResponseDto>(avatar);
            SetFullNamesForUserAndRelatedEntities(response); ;
            return Success(response);
        }

        public ServiceResult<List<AvatarResponseDto>> GetAllAvatar()
        {
            var avatars = _avatarRepository.GetAll().ToList();

            List<AvatarResponseDto> response = avatars
                                .Select(u =>
                                {
                                    var avatar = new AvatarResponseDto()
                                    {
                                        Id = u.Id,
                                        FileName = u.FileName,
                                        MimeType = u.MimeType,
                                        FileSize = u.FileSize,
                                        IsPublished = u.IsPublished,
                                        CreatedAt = u.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        UpdatedAt = u.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt"),
                                        CreatedBy = u.CreatedBy.ToString(),
                                        UpdatedBy = u.UpdatedBy.ToString(),
                                    };

                                    SetFullNamesForUserAndRelatedEntities(avatar);
                                    return avatar;
                                }).ToList();

            return Success(response);
        }

        private void SetFullNamesForUserAndRelatedEntities(AvatarResponseDto re)
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
