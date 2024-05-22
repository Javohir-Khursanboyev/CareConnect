using FluentValidation;
using CareConnect.Service.Helpers;
using CareConnect.Service.DTOs.Users;

namespace CareConnect.Service.Validators.Users;

public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
{
    public UserCreateModelValidator()
    {
        RuleFor(user => user.FirstName)
            .NotNull()
            .WithMessage(user => $"{nameof(user.FirstName)} is not specified");

        RuleFor(user => user.LastName)
           .NotNull()
           .WithMessage(user => $"{nameof(user.LastName)} is not specified");

        RuleFor(user => user.Email)
            .Must(ValidationHelper.IsEmailValid);

        RuleFor(user => user.Password)
            .Must(ValidationHelper.IsPasswordHard);

        RuleFor(user => user.Phone)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Phone)} is not specified");

        RuleFor(user => user.Gender)
            .NotNull()
            .WithMessage(user => $"{nameof(user.Gender)} is not specified");

        RuleFor(user => user.DateOfBirth)
            .NotNull()
            .WithMessage(user => $"{nameof(user.DateOfBirth)} is not specified");
    }
}