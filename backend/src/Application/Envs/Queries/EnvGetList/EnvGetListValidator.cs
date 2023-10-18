using FluentValidation;

namespace KwikDeploy.Application.Envs.Queries.EnvGetList;

public class EnvGetListValidator : AbstractValidator<EnvGetList>
{
    public EnvGetListValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
    }
}