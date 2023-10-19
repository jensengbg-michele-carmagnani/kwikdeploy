using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Commands;

public record UserSetEmail : IRequest
{
    [FromRoute]
    public string Id { get; init; } = default!;

    [FromBody]
    public UserSetEmailBody Body { get; init; } = null!;
}

public record UserSetEmailBody
{
    public string Email { get; init; } = default!;
}

public class UserSetEmailHandler : IRequestHandler<UserSetEmail>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserSetEmailHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UserSetEmail request, CancellationToken cancellationToken)
    {
        ApplicationUser? user = await _userManager.FindByIdAsync(request.Id);
        if (user is null)
        {
            throw new NotFoundException(nameof(UserSetEmail.Id), request.Id);
        }

        string normalizedEmail = _userManager.NormalizeEmail(request.Body.Email);
        if (user.NormalizedEmail.Equals(normalizedEmail))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new(nameof(request.Body.Email), "The new email address must be different from the current one.")
            });
        }

        if (await _userManager.Users.AnyAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new(nameof(request.Body.Email), "Another user with the same e-mail already exists.")
            });
        }

        IdentityResult result = await _userManager.SetEmailAsync(user, request.Body.Email);
        if (!result.Succeeded)
        {
            var validationErrors = result.Errors.Select(x => new ValidationFailure("", $"{x.Code}: ${x.Description}"));
            throw new ValidationException(new List<ValidationFailure>(validationErrors));
        }
    }
}