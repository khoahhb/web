using AutoMapper;
using Web.Application.Helpers.DateTimeHandlers;
using Web.Application.Helpers.GeneratePassword;
using Web.Domain.Entities;
using Web.Model.Dtos.Avatar.Request;
using Web.Model.Dtos.Avatar.Response;
using Web.Model.Dtos.User.Request;
using Web.Model.Dtos.User.Response;
using Web.Model.Dtos.UserProfile.Request;
using Web.Model.Dtos.UserProfile.Response;

namespace Web.Application.Helpers.AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<CreateAvatarRequestDto, Avatar>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
                .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => src.IsPublished == null ? false : true));

            CreateMap<CreateUserRequestDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHandler.HashPassword(src.Password)))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToUtc(TimeZoneInfo.Local.Id, "dd/MM/yyyy")));

            CreateMap<CreateUserProfileRequestDto, UserProfile>();

            CreateMap<UpdateAvatarRequestDto, Avatar>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateUserRequestDTO, User>()
                .ForMember(dest => dest.Username,
                    opt =>
                    {
                        //opt.Condition(src => !string.IsNullOrEmpty(src.Username));
                        opt.Condition(src => false);
                        opt.MapFrom(src => src.Username);
                    })
                .ForMember(dest => dest.Password,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Password));
                        opt.MapFrom(src => PasswordHandler.HashPassword(src.Password));
                    })
                .ForMember(dest => dest.DateOfBirth,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.DateOfBirth));
                        opt.MapFrom(src => src.DateOfBirth.ToUtc(TimeZoneInfo.Local.Id, "dd/MM/yyyy"));
                    })
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    if (srcMember is Guid guid)
                        return false;
                    return srcMember != null;
                }));

            CreateMap<UpdateUserProfileRequestDto, UserProfile>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Avatar, AvatarResponseDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")));

            CreateMap<UserProfile, UserProfileResponseDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")));

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(s => s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy") : null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(s => s.CreatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(s => s.UpdatedAt.ToFormattedString(TimeZoneInfo.Local.Id, false, "dd/MM/yyyy h:mm:ss tt")));
        }
    }
}
