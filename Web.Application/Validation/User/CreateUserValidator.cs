using FluentValidation;
using System.Globalization;
using Web.Infracturre.Repositories.AvatarRepo;
using Web.Infracturre.Repositories.UserProfileRepo;
using Web.Infracturre.Repositories.UserRepo;
using Web.Model.Dtos.User.Request;

namespace Web.Application.Validation.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequestDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IAvatarRepository _avatarRepository;

        public CreateUserValidator(IUserRepository userRepository, IUserProfileRepository userProfileRepository, IAvatarRepository avatarRepository)
        {
            _userRepository = userRepository;
            _userProfileRepository = userProfileRepository;
            _avatarRepository = avatarRepository;

            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username is required.")
                .Length(6, 50).WithMessage("Username must be between 6 and 50 characters.")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("Username can only have alphabets, numbers and _.")
                .Must(CheckUsernameExist)
                .WithMessage("Username existed.")
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 100).WithMessage("Password must be between 6 and 100 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain one or more capital letters.")
                .Matches("[a-z]").WithMessage("Password must contain one or more lowercase letters.")
                .Matches(@"\d").WithMessage("Password must contain one or more digits.")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Password' must contain one or more special characters.")
                .Matches("^[^£# “”]*$").WithMessage("Password must not contain the following characters £ # “” or spaces.");

            RuleFor(x => x.Fullname)
                .NotNull().WithMessage("Fullname is required.")
                .NotEmpty().WithMessage("Fullname is required.")
                .Length(6, 255)
                .WithMessage("Fullname must be between 6 and 255 characters.");

            RuleFor(x => x.DateOfBirth)
                .Must(CheckDateFormat).WithMessage("Date of Birth must be in the format dd/MM/yyyy")
                .When(x => !string.IsNullOrEmpty(x.DateOfBirth))
                .Must(CheckDateOfBirth).WithMessage("Age must be between 18 and 100")
                .When(x => !string.IsNullOrEmpty(x.DateOfBirth));

            RuleFor(x => x.Phone)
                .Matches("^[0-9\\-\\+]{9,15}$")
                .WithMessage("Phone must be a valid international phone number.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Email is required.")
                .NotEmpty().WithMessage("Email is required.")
                .Length(12, 255).WithMessage("Email must be between 12 and 255 characters.")
                .EmailAddress().WithMessage("Email is not a valid email address.")
                .Must(CheckEmailExist)
                .WithMessage("Email existed.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.AvatarId)
                .Must(CheckAvatarValid)
                .WithMessage("Avatar Id is not published or do not exist.")
                .When(x => x.AvatarId != null && x.AvatarId != Guid.Empty);

            RuleFor(x => x.UserProfileId)
                .NotNull().WithMessage("UserProfile is required.")
                .NotEmpty().WithMessage("UserProfile is required.")
                .Must(CheckProfileExist)
                .WithMessage("UserProfile do not exist.")
                .When(x => x.UserProfileId != null || x.UserProfileId != Guid.Empty);
        }

        private bool CheckUsernameExist(string value)
        {
            return _userRepository.GetByUsername(value) == null;
        }

        private bool CheckEmailExist(string value)
        {
            return _userRepository.GetByEmail(value) == null;
        }

        private bool CheckAvatarValid(Guid? value)
        {
            return _avatarRepository.GetOne(a => a.Id == value && a.IsPublished ==true) != null;
        }

        private bool CheckProfileExist(Guid? value)
        {
            return _userProfileRepository.GetOneById(value) != null;
        }

        private bool CheckDateFormat(string value)
        {
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        private bool CheckDateOfBirth(string value)
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
