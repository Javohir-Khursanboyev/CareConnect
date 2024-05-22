using FluentValidation;
using CareConnect.Service.Helpers;
using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.Validators.Users;

public class UserChangePasswordModelValidator : AbstractValidator<UserChangePasswordModel>
{
    public UserChangePasswordModelValidator()
    {
        RuleFor(up => up.OldPassword)
            .NotNull()
            .WithMessage(up => $"{nameof(up.OldPassword)} is not specified");

        RuleFor(up => up.NewPassword)
            .Must(ValidationHelper.IsPasswordHard);

        RuleFor(up => up.ConfirmPassword)
            .NotNull()
            .WithMessage(up => $"{nameof(up.OldPassword)} is not specified");
    }
}
