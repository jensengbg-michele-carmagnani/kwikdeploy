using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Users.Queries.UserUniqueNameQuery;

public class UserUniqueUserNameQuery : IRequest<Result<bool>>
{
    [FromQuery]
    public string UserName { get; set; } = default!;

    [FromQuery]
    public string? Id { get; set; }
}

public class UserUniqueUserNameQueryHandler : IRequestHandler<UserUniqueUserNameQuery, Result<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserUniqueUserNameQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(UserUniqueUserNameQuery request, CancellationToken cancellationToken)
    {
        string normalizedUserName = _userManager.NormalizeName(request.UserName);

        var result = !await _userManager.Users.AnyAsync(x => x.NormalizedUserName == normalizedUserName && x.Id != request.Id,
            cancellationToken);

        return new Result<bool>(result);
    }
}