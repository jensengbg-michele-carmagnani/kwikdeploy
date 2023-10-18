using FluentValidation;


namespace KwikDeploy.Application.Envs.Commands.EnvUpdate;
public class EnvUpdateCommandValidator : AbstractValidator<EnvUpdateCommand>
{
    public EnvUpdateCommandValidator()
    {
        RuleFor(v => v.Body.Name)
            .MaximumLength(30)
            .NotEmpty();
    }
}
