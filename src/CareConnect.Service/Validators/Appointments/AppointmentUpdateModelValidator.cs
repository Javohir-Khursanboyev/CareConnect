using FluentValidation;
using CareConnect.Service.DTOs.Appointments;

namespace CareConnect.Service.Validators.Appointments;

public class AppointmentUpdateModelValidator : AbstractValidator<AppointmentUpdateModel>
{
    public AppointmentUpdateModelValidator()
    {
        RuleFor(a => a.DoctorId)
            .NotNull()
            .WithMessage(a => $"{nameof(a.DoctorId)} is not specified");

        RuleFor(a => a.PatientId)
           .NotNull()
           .WithMessage(a => $"{nameof(a.PatientId)} is not specified");

        RuleFor(a => a.Duration)
            .NotNull()
            .WithMessage(a => $"{nameof(a.Duration)} is not specified");

        RuleFor(a => a.Date)
            .NotNull()
            .WithMessage(a => $"{nameof(a.Date)} is not specified");

        RuleFor(a => a.Status)
            .NotNull()
            .IsInEnum()
            .WithMessage(a => $"{nameof(a.Status)} is not specified");
    }
}
