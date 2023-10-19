using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Queries;

public class UserUniqueEmail : IRequest<Result<bool>>
{
    [FromQuery]
    public string Email { get; set; } = default!;

    [FromQuery]
    public string? Id { get; set; }
}

public class UserUniqueEmailHandler : IRequestHandler<UserUniqueEmail, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserUniqueEmailHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(UserUniqueEmail request, CancellationToken cancellationToken)
    {
        string normalizedEmail = _userManager.NormalizeEmail(request.Email);

        var user = !await _userManager.Users.AnyAsync(x => x.NormalizedEmail == normalizedEmail && x.Id != request.Id,
            cancellationToken);

        return new Result<bool>(user);
    }
}