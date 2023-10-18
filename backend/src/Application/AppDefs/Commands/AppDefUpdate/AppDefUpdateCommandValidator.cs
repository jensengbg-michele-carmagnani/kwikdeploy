using FluentValidation;

namespace KwikDeploy.Application.AppDefs.Commands.AppDefUpdate;

public class AppDefUpdateCommandValidator : AbstractValidator<AppDefUpdateCommand>
{
    public AppDefUpdateCommandValidator()
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
