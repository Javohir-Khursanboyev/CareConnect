using FluentValidation;
using CareConnect.Service.DTOs.Doctors;

namespace CareConnect.Service.Validators.Doctors;

public class DoctorCreateModelValidator : AbstractValidator<DoctorCreateModel>
{
    public DoctorCreateModelValidator()
    {
        RuleFor(d => d.About)
            .NotNull()
            .WithMessage(d => $"{nameof(d.About)} is not specified");

        RuleFor(d => d.Specialty)
            .NotNull()
            .WithMessage(d => $"{nameof(d.Specialty)} is not specified");

        RuleFor(d => d.DepartmentId)
            .NotNull()
            .WithMessage(d => $"{nameof(d.DepartmentId)} is not specified");
    }
}
