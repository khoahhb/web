using FluentValidation;
using Web.Infracturre.Repositories.UserProfileRepo;
using Web.Model.Dtos.UserProfile.Request;

namespace Web.Application.Validation.UserProfile
{
    public class UpdateUserProfileValidator : AbstractValidator<UpdateUserProfileRequestDto>
    {

        private readonly IUserProfileRepository _userProfileRepository;

        public UpdateUserProfileValidator(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;

            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required.")
                .NotEmpty().WithMessage("Id is required.")
                .MustAsync(async (id, _) =>
                {
                    return await _userProfileRepository.GetOneById(id) != null;
                })
                .WithMessage("Profile id do not exist.");

            RuleFor(x => x.Name)
                .Length(6, 50).WithMessage("Name must be between 6 and 50 characters.");

            RuleFor(x => x.Descrtiption)
                .Length(6, 50).WithMessage("Descrtiption must be between 6 and 1000 characters.");
        }
    }
}