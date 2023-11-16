using FluentValidation;
using Web.Application.Helpers;
using Web.Model.Dtos.RequestDtos.Avatar;

namespace Web.Application.Validation.Avatar
{
    public class CreateAvatarValidator : AbstractValidator<CreateAvatarRequestDto>
    {
        private readonly PhotoSettings _settings;

        public CreateAvatarValidator()
        {
            _settings = new PhotoSettings() { MaxBytes = 2048000 };

            RuleFor(x => x.File)
                .NotEmpty().WithMessage("Please attach your avatar.")
                .NotNull().WithMessage("Please attach your avatar.");

            RuleFor(x => x.File.Length)
                .ExclusiveBetween(0, _settings.MaxBytes)
                .WithMessage($"Avatar size should be greater than 0 and less than {_settings.MaxBytes / 1024 / 1024} MB")
                .When(x => x.File != null);

            RuleFor(x => x.File.FileName)
                .Must(_settings.IsSupported)
                .WithMessage("Unsupported avatar type.")
                .When(f => f.File != null);
        }
    }
}
