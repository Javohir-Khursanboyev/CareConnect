using FluentValidation;
using CareConnect.Service.Helpers;
using CareConnect.Service.DTOs.Assets;

namespace CareConnect.Service.Validators.Assets;

public class AssetCreateModelValidator : AbstractValidator<AssetCreateModel>
{
    public AssetCreateModelValidator()
    {
        RuleFor(asset => asset.FileType)
            .NotNull()
            .IsInEnum()
            .WithMessage(asset => $"{nameof(asset.FileType)} is not specified");

        RuleFor(asset => asset.File)
            .Must(ValidationHelper.IsFileValid);
    }
}
