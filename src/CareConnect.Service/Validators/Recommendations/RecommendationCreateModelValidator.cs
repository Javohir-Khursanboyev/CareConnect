using FluentValidation;
using CareConnect.Domain.Entities.Recommendations;

namespace CareConnect.Service.Validators.Recommendations;

public class RecommendationCreateModelValidator : AbstractValidator<RecommendationsCreateModel>
{
    public RecommendationCreateModelValidator()
    {
        RuleFor(r => r.AppointmentId)
           .NotNull()
           .WithMessage(r => $"{nameof(r.AppointmentId)} is not specified");

        RuleFor(r => r.Prescription)
            .NotNull()
            .WithMessage(r => $"{nameof(r.Prescription)} is not specified");
    }
}