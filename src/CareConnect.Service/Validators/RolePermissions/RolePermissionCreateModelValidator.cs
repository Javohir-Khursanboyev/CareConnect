using FluentValidation;
using CareConnect.Service.DTOs.RolePermissions;

namespace CareConnect.Service.Validators.RolePermissions;

public class RolePermissionCreateModelValidator : AbstractValidator<RolePermissionCreateModel>
{
    public RolePermissionCreateModelValidator()
    {
        RuleFor(asset => asset.RoleId)
            .NotNull()
            .WithMessage(asset => $"{nameof(asset.RoleId)} is not specified");

        RuleFor(asset => asset.PermissionId)
           .NotNull()
           .WithMessage(asset => $"{nameof(asset.PermissionId)} is not specified");
    }
}
