using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Targets.Commands.TargetCreate;
using KwikDeploy.Application.Targets.Commands.TargetDelete;
using KwikDeploy.Application.Targets.Commands.TargetRegenerateKey;
using KwikDeploy.Application.Targets.Commands.TargetUpdate;
using KwikDeploy.Application.Targets.Queries.TargetGet;
using KwikDeploy.Application.Targets.Queries.TargetGetList;
using KwikDeploy.Application.Targets.Queries.TargetUniqueName;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Targets/{ProjectId}")]
public class TargetsController : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedList<TargetHeadDto>>> GetList(TargetGetList query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<TargetDto>> GetById(TargetGetQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniqueName")]
    public async Task<ActionResult<Result<bool>>> IsUniqueName(TargetUniqueNameQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Result<int>>> Create(TargetCreateCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(TargetUpdateCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(TargetDeleteCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("{Id}/regenerateKey")]
    public async Task<ActionResult<Result<string>>> RegenerateKey(TargetRegenerateKeyCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }
}
