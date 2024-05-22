using FluentValidation;
using CareConnect.Service.DTOs.Permissions;

namespace CareConnect.Service.Validators.Permissions;

public class PermissionCreateModelValidator : AbstractValidator<PermissionCreateModel>
{
    public PermissionCreateModelValidator()
    {
        RuleFor(role => role.Action)
            .NotNull()
            .WithMessage(role => $"{nameof(role.Action)} is not specified");

        RuleFor(role => role.Controller)
           .NotNull()
           .WithMessage(role => $"{nameof(role.Controller)} is not specified");
    }
}
