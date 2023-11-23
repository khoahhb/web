using FluentValidation;
using Web.Application.Helpers;
using Web.Infracturre.Repositories.AvatarRepo;
using Web.Model.Dtos.Avatar.Request;

namespace Web.Application.Validation.Avatar
{
    public class UpdateAvatarValidator : AbstractValidator<UpdateAvatarRequestDto>
    {
        private readonly PhotoSettings _settings;

        private readonly IAvatarRepository _avatarRepository;

        public UpdateAvatarValidator(IAvatarRepository avatarRepository)
        {
            _settings = new PhotoSettings() { MaxBytes = 2048000 };

            _avatarRepository = avatarRepository;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Avatar Id is required.")
                .Must(CheckAvatarExist)
                .WithMessage("Avatar id do not exist.");

            RuleFor(x => x.File.Length)
                .ExclusiveBetween(0, _settings.MaxBytes)
                .WithMessage($"Avatar size should be greater than 0 and less than {_settings.MaxBytes / 1024 / 1024} MB")
                .When(x => x.File != null);

            RuleFor(x => x.File.FileName)
                .Must(_settings.IsSupported)
                .WithMessage("Unsupported avatar type.")
                .When(f => f.File != null);
        }

        private bool CheckAvatarExist(Guid id)
        {
            return _avatarRepository.GetOneById(id) != null;
        }
    }

}
