using KwikDeploy.Api.Controllers;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Users.Commands.UserCreate;
using KwikDeploy.Application.Users.Commands.UserDelete;
using KwikDeploy.Application.Users.Commands.UserSetEmail;
using KwikDeploy.Application.Users.Commands.UserSetName;
using KwikDeploy.Application.Users.Queries.UserGet;
using KwikDeploy.Application.Users.Queries.UserGetList;
using KwikDeploy.Application.Users.Queries.UserUniqueEmailQuery;
using KwikDeploy.Application.Users.Queries.UserUniqueNameQuery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Users.Queries.UserGet;

namespace Api.Controllers;

[Route("Users")]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserHeadDto>>> GetList(UserGetListQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDto>> GetById(UserGetQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniqueUserName")]
    public async Task<ActionResult<Result<bool>>> IsUniqueUserName(UserUniqueUserNameQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniqueEmail")]
    public async Task<ActionResult<Result<bool>>> IsUniqueEmail(UserUniqueEmailQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Result<string>>> Create(UserCreateCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> Delete(UserDeleteCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{Id}/email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> SetEmail(UserSetEmailCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{Id}/userName")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> SetUserName(UserSetUserNameCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}