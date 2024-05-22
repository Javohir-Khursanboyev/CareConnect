using FluentValidation;
using CareConnect.Service.DTOs.Departments;

namespace CareConnect.Service.Validators.Departments;

public class DepartmentUpdateModelValidator : AbstractValidator<DepartmentCreateModel>
{
    public DepartmentUpdateModelValidator()
    {
        RuleFor(d => d.Name)
            .NotNull()
            .WithMessage(d => $"{nameof(d.Name)} is not specified");

        RuleFor(d => d.HospitalId)
            .NotNull()
            .WithMessage(d => $"{nameof(d.Name)} is not specified"); ;
    }
}