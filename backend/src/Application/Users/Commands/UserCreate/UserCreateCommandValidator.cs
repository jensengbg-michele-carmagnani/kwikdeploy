using FluentValidation;

namespace KwikDeploy.Application.Users.Commands.UserCreate;

public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(v => v.Body.UserName).MaximumLength(255).NotEmpty();
        RuleFor(v => v.Body.Email).MaximumLength(255).EmailAddress();
        RuleFor(v => v.Body.Password).MaximumLength(255).NotEmpty();
        RuleFor(v => v.Body.ConfirmPassword)
            .MaximumLength(255).NotEmpty().Equal(v => v.Body.Password).WithMessage("ConfirmPassword must be equal to Password.");
    }
}