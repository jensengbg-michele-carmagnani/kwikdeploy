using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Exceptions;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Commands.UserCreate;

public record UserCreateCommand : IRequest<Result<string>>
{
    [FromBody]
    public UserCreateCommandBody Body { get; init; } = null!;
}

public record UserCreateCommandBody
{
    public string UserName { get; set; } = default!;
    public string? Email { get; set; }
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}

public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Result<string>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserCreateCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        string normalizedUserName = _userManager.NormalizeName(request.Body.UserName);
        if (await _userManager.Users.AnyAsync(x => x.NormalizedUserName == normalizedUserName, cancellationToken))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new(nameof(request.Body.UserName), "Another user with the same name already exists.")
            });
        }

        if (request.Body.Email is not null)
        {
            var normalizedEmail = _userManager.NormalizeEmail(request.Body.Email);
            if (await _userManager.Users.AnyAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken))
            {
                throw new ValidationException(new List<ValidationFailure>
                {
                    new(nameof(request.Body.Email), "Another user with the same e-mail already exists.")
                });
            }
        }

        IdentityResult result = await _userManager.CreateAsync(new ApplicationUser
        {
            UserName = request.Body.UserName,
            Email = request.Body.Email,
            NormalizedUserName = normalizedUserName,
            NormalizedEmail = _userManager.NormalizeEmail(request.Body.Email)
        });

        if (!result.Succeeded)
        {
            IEnumerable<string> err = result.Errors.Select(x => x.Code);
            throw new UserCreateErrorException();
        }

        ApplicationUser? user = await _userManager.FindByNameAsync(normalizedUserName);

        return new Result<string>(user!.Id);
    }
}