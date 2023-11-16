using AutoMapper;
using System.Globalization;
using Web.Domain.Entities;
using Web.Domain.Helpers;
using Web.Model.Dtos.RequestDtos.Avatar;
using Web.Model.Dtos.RequestDtos.User;
using Web.Model.Dtos.RequestDtos.UserProfile;
using Web.Model.Dtos.ResponseDtos;
using Profile = AutoMapper.Profile;

namespace Web.Application.Helpers
{
    public class AutoMapperProfile : Profile
    {
        static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<AvatarResponseDto, Avatar>().ReverseMap();
                cfg.CreateMap<ProfileResponseDto, UserProfile>().ReverseMap();
            });
            var mapper = new Mapper(config);
            return mapper;
        }
        public AutoMapperProfile()
        {
            var mapper = InitializeAutomapper();

            //Create
            CreateMap<CreateAvatarRequestDto, Avatar>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => src.IsPublished == null ? false : true));

            CreateMap<CreateUserRequestDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHandler.HashPassword(src.Password)))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.DateOfBirth,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.DateOfBirth));
                        opt.MapFrom(src => ConvertToDateTime(src.DateOfBirth));
                    })
                .AfterMap((src, dest) =>
                {
                    var newGuid = Guid.NewGuid();
                    dest.Id = newGuid;
                });

            CreateMap<CreateUserProfileRequestDto, UserProfile>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            //Update
            CreateMap<UpdateAvatarRequestDto, Avatar>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.MimeType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.File.Length))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateUserRequestDTO, User>()
                .ForMember(dest => dest.Password,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Password));
                        opt.MapFrom(src => PasswordHandler.HashPassword(src.Password));
                    })
                .ForMember(dest => dest.Fullname,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Fullname));
                        opt.MapFrom(src => src.Fullname);
                    })
                .ForMember(dest => dest.Gender,
                    opt =>
                    {
                        opt.Condition(src => src.Gender != null);
                        opt.MapFrom(src => src.Gender);
                    })
                .ForMember(dest => dest.DateOfBirth,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.DateOfBirth));
                        opt.MapFrom(src => ConvertToDateTime(src.DateOfBirth));
                    })
                .ForMember(dest => dest.Phone,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Phone));
                        opt.MapFrom(src => src.Phone);
                    })
                .ForMember(dest => dest.Email,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Email));
                        opt.MapFrom(src => src.Email);
                    })
                .ForMember(dest => dest.Address,
                    opt =>
                    {
                        opt.Condition(src => !string.IsNullOrEmpty(src.Address));
                        opt.MapFrom(src => src.Address);
                    })
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UserProfileId,
                    opt =>
                    {
                        opt.Condition(src => src.UserProfileId != null);
                        opt.MapFrom(src => src.UserProfileId);
                    })
                .ForMember(dest => dest.AvatarId,
                    opt =>
                    {
                        opt.Condition(src => src.AvatarId != null);
                        opt.MapFrom(src => src.AvatarId);
                    });

            CreateMap<UpdateUserProfileRequestDto, UserProfile>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            //Response
            CreateMap<AvatarResponseDto, Avatar>().ReverseMap();

            CreateMap<ProfileResponseDto, UserProfile>().ReverseMap();

            
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(s => ConvertIso8601ToDateTime(s.DateOfBirth)))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(s => mapper.Map<AvatarResponseDto>(s.Avatar)))
                .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(s => mapper.Map<ProfileResponseDto>(s.UserProfile)));

            CreateMap<UserResponseDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(s => ConvertToDateTime(s.DateOfBirth)))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(s => mapper.Map<Avatar>(s.Avatar)))
                .ForMember(dest => dest.UserProfile, opt => opt.MapFrom(s => mapper.Map<UserProfile>(s.UserProfile)));
        }
        public DateTime ConvertToDateTime(string dateString)
        {
            DateTime parsedDate = DateTime.ParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
            return DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
        }
        public string ConvertIso8601ToDateTime(DateTime? iso8601Date)
        {
            if (iso8601Date.HasValue)
            {
                return iso8601Date.Value.ToUniversalTime().ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
