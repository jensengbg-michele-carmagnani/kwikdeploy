using System.Threading;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Application.Projects.Commands.ProjectCreate;
using KwikDeploy.Application.Projects.Commands.ProjectDelete;
using KwikDeploy.Application.Projects.Commands.ProjectUpdate;
using KwikDeploy.Application.Projects.Queries.ProjectGet;
using KwikDeploy.Application.Projects.Queries.ProjectGetList;
using KwikDeploy.Application.Projects.Queries.ProjectUniqueName;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Api.Controllers;

[Route("Projects")]
public class ProjectsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProjectHeadDto>>> GetList(ProjectGetListQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ProjectDto>> GetById(ProjectGetQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpGet("uniquename")]
    public async Task<ActionResult<Result<bool>>> IsUniqueName(ProjectUniqueNameQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public async Task<ActionResult<Result<int>>> Create(ProjectCreateCommand command, CancellationToken cancellationToken)
    {
        return await Mediator.Send(command, cancellationToken);
    }

    [HttpPut("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Update(ProjectUpdateCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Delete(ProjectDeleteCommand command, CancellationToken cancellationToken)
    {
        await Mediator.Send(command, cancellationToken);

        return NoContent();
    }
}
