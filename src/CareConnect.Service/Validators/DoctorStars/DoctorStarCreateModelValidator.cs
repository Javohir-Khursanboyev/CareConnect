using FluentValidation;
using CareConnect.Service.DTOs.DoctorStars;

namespace CareConnect.Service.Validators.DoctorStars;

public class DoctorStarCreateModelValidator : AbstractValidator<DoctorStarCreateModel>
{
    public DoctorStarCreateModelValidator()
    {
        RuleFor(ds => ds.DoctorId)
            .NotNull()
            .WithMessage(ds => $"{nameof(ds.DoctorId)} is not specified");

        RuleFor(ds => ds.PatientId)
            .NotNull()
            .WithMessage(ds => $"{nameof(ds.PatientId)} is not specified");

        RuleFor(ds => ds.Star)
            .NotNull()
            .WithMessage(ds => $"{nameof(ds.Star)} is not specified");
    }       
}