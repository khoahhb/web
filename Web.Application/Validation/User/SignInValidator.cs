using FluentValidation;
using Web.Infracturre.Repositories.UserRepo;
using Web.Model.Dtos.User.Request;

namespace Web.Application.Validation.User
{
    public class SignInValidator : AbstractValidator<SignInRequestDto>
    {

        private readonly IUserRepository _userRepository;

        public SignInValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username)
                .NotNull().WithMessage("Username is required.")
                .NotEmpty().WithMessage("Username is required.")
                .Length(6, 50).WithMessage("Username must be between 6 and 50 characters.")
                .Matches("^[a-zA-Z0-9_]*$").WithMessage("Username can only have alphabets, numbers and _.")
                .MustAsync(async (username, _) =>
                    await _userRepository.GetUserByUsername(username) != null)
                .WithMessage("Username not exist.")
                .When(x => !string.IsNullOrEmpty(x.Username));

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Password is required.")
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
