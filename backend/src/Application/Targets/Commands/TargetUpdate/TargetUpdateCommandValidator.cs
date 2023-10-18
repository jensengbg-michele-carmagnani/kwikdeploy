using FluentValidation;

namespace KwikDeploy.Application.Targets.Commands.TargetUpdate;

public class TargetUpdateCommandValidator : AbstractValidator<TargetUpdateCommand>
{
    public TargetUpdateCommandValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
