using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Targets.Queries.TargetGet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Queries.EnvGet;

public record EnvGetQuery : IRequest<EnvDto>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class EnvGetHandler : IRequestHandler<EnvGetQuery, EnvDto>
{
    private readonly IApplicationDbContext _context;

    public EnvGetHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<EnvDto> Handle(EnvGetQuery request, CancellationToken cancellationToken)
    {
        var targetDto = await _context.Envs.Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                                    .Select(x => new EnvDto
                                    {
                                        Id = x.Id,
                                        ProjectId = x.ProjectId,
                                        TargetId = x.TargetId,
                                        Name = x.Name,
                                    }).SingleOrDefaultAsync();

        if (targetDto is null)
        {
            throw new NotFoundException(nameof(TargetDto), request.Id);
        }

        return targetDto;
    }
}