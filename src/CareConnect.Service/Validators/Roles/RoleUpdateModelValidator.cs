using FluentValidation;
using CareConnect.Service.DTOs.Roles;

namespace CareConnect.Service.Validators.Roles;

public class RoleUpdateModelValidator : AbstractValidator<RoleUpdateModel>
{
    public RoleUpdateModelValidator()
    {
        RuleFor(role => role.Name)
            .NotNull()
            .WithMessage(role => $"{nameof(role.Name)} is not specified");
    }
}
