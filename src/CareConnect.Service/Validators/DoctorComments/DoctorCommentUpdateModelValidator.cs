using FluentValidation;
using CareConnect.Service.DTOs.DoctorComments;

namespace CareConnect.Service.Validators.DoctorComments;

public class DoctorCommentUpdateModelValidator : AbstractValidator<DoctorCommentUpdateModel>
{
    DoctorCommentUpdateModelValidator()
    {
        RuleFor(dc => dc.DoctorId)
             .NotNull()
             .WithMessage(dc => $"{nameof(dc.DoctorId)} is not specified");

        RuleFor(dc => dc.PatientId)
            .NotNull()
            .WithMessage(dc => $"{nameof(dc.PatientId)} is not specified");

        RuleFor(dc => dc.Comment)
            .NotNull()
            .WithMessage(dc => $"{nameof(dc.Comment)} is not specified");
    }
}