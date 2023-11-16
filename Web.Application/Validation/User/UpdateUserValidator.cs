using FluentValidation;
using Web.Infracturre.Interfaces;
using Web.Model.Enum;
using System.Reflection.Metadata.Ecma335;
using Web.Model.Dtos.RequestDtos.User;
using System.Globalization;

namespace Web.Application.Validation.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserRequestDTO>
    {

        private Guid userId;

        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAvatarRepository _avatarRepository;

        public UpdateUserValidator(IUserRepository userRepository, IUserProfileRepository userProfileRepository, IAvatarRepository avatarRepository)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _avatarRepository = avatarRepository;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, _) =>
                {
                    userId = id;
                    return await _userRepository.GetUserById(id) != null;
                })
                .WithMessage("User id do not exist.");

            RuleFor(x => x.Password)
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password' must contain one or more special characters.")
                .Matches("^[^£# “”]*$")
                .WithMessage("Password must not contain the following characters £ # “” or spaces.")
                .When(x => !string.IsNullOrEmpty(x.Password));

            RuleFor(x => x.Fullname)
                .Length(6, 255)
                .WithMessage("Fullname must be between 6 and 255 characters.");

            RuleFor(x => x.DateOfBirth)
                .Must(BeAValidDate).WithMessage("Date of Birth must be in the format dd/MM/yyyy")
                .When(x => !string.IsNullOrEmpty(x.DateOfBirth))
                .Must(BeWithinAgeLimit).WithMessage("Age must be between 18 and 100")
                .When(x => !string.IsNullOrEmpty(x.DateOfBirth));

            RuleFor(x => x.Phone)
                .Matches("^[0-9\\-\\+]{9,15}$")
                .WithMessage("Phone must be a valid international phone number.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Email)
                .Length(12, 255).WithMessage("Email must be between 12 and 255 characters.")
                .EmailAddress().WithMessage("Email is not a valid email address.")
                .MustAsync(async (email, _) =>
                {
                    var user = await _userRepository.GetUserByEmail(email);
                    if (user != null)
                    {
                        return user.Id == userId;
                    }
                    return true;
                })
                .WithMessage("Email existed.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.AvatarId)
               .MustAsync(async (id, _) =>
               {
                   return await _avatarRepository.GetAvatarByCondition(
                       prof => prof.Id == id && prof.IsPublished == true) != null;
               })
               .WithMessage("Avatar Id is not published or do not exist.")
               .When(x => x.AvatarId != null && x.AvatarId != Guid.Empty);

            RuleFor(x => x.UserProfileId)
                .MustAsync(async (id, _) =>
                {
                    return id != null ? await _userProfileRepository.GetUserProfileById(id) != null : false;
                })
                .WithMessage("UserProfileId do not exist.")
                .When(x => x.UserProfileId != null && x.UserProfileId != Guid.Empty);
        }
        private bool BeAValidDate(string value)
        {
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool BeWithinAgeLimit(string value)
        {
            if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                var age = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;

                return age >= 18 && age <= 100;
            }
            return false;
        }
    }
}
