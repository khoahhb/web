using FluentValidation;
using Web.Model.Dtos.RequestDtos.UserProfile;

namespace Web.Application.Validation.UserProfile
{
    public class CreateUserProfileValidator : AbstractValidator<CreateUserProfileRequestDto>
    {
        public CreateUserProfileValidator()
        {

            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.")
                .NotEmpty().WithMessage("Name is required.")
                .Length(6, 50).WithMessage("Name must be between 6 and 50 characters.");

            RuleFor(x => x.Descrtiption)
                .Length(6, 50).WithMessage("Descrtiption must be between 6 and 1000 characters.");
        }
    }
}
