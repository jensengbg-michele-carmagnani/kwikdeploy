using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Domain.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Users.Queries.UserGet;

namespace KwikDeploy.Application.Users.Queries.UserGet;

public class UserGetQuery : IRequest<UserDto>
{
    [FromRoute]
    public string Id { get; set; } = default!;
}

public class UserGetQueryHandler : IRequestHandler<UserGetQuery, UserDto>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserGetQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;

    }

    public async Task<UserDto> Handle(UserGetQuery request, CancellationToken cancellationToken)
    {
        UserDto? userDto = await _userManager.Users.Select(x => new UserDto
        {
            Id = x.Id,
            UserName = x.UserName,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            LockoutEnabled = x.LockoutEnabled,
            LockoutEnd = x.LockoutEnd,
            AccessFailedCount = x.AccessFailedCount
        }).FirstOrDefaultAsync(predicate: x => x.Id == request.Id, cancellationToken);

        if (userDto is null)
        {
            throw new NotFoundException(nameof(UserDto), request.Id);
        }

        return userDto;
    }
}