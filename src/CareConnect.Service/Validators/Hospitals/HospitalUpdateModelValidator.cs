using FluentValidation;
using CareConnect.Service.Helpers;
using CareConnect.Service.DTOs.Hospitals;

namespace CareConnect.Service.Validators.Hospitals;

public class HospitalUpdateModelValidator : AbstractValidator<HospitalUpdateModel>
{
    public HospitalUpdateModelValidator()
    {
        RuleFor(h => h.Name)
            .NotNull()
            .WithMessage(h => $"{nameof(h.Name)} is not specified");

        RuleFor(h => h.Address)
            .NotNull()
            .WithMessage(h => $"{nameof(h.Address)} is not specified");

        RuleFor(h => h.Phone)
            .NotNull()
            .WithMessage(h => $"{nameof(h.Phone)} is not specified");

        RuleFor(h => h.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(h => h.Description)
            .NotNull()
            .WithMessage(h => $"{nameof(h.Description)} is not specified");
    }
}