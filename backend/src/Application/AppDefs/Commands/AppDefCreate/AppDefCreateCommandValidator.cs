using FluentValidation;

namespace KwikDeploy.Application.AppDefs.Commands.AppDefCreate;

public class AppDefCreateCommandValidator : AbstractValidator<AppDefCreateCommand>
{
    public AppDefCreateCommandValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();

        RuleFor(v => v.Body.ImageName)
           .MaximumLength(200)
           .NotEmpty();

        RuleFor(v => v.Body.Tag)
           .MaximumLength(200)
           .NotEmpty();
    }
}
