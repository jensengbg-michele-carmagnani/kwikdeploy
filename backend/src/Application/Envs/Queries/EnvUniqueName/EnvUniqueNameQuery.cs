using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Queries.EnvUniqueName;

public record EnvUniqueNameQuery : IRequest<Result<bool>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromQuery]
    public string Name { get; init; } = null!;

    [FromQuery]
    public int? EnvId { get; init; } = null;
}

public class EnvUniqueNameQueryHandler : IRequestHandler<EnvUniqueNameQuery, Result<bool>>
{
    private readonly IApplicationDbContext _context;

    public EnvUniqueNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(EnvUniqueNameQuery request, CancellationToken cancellationToken)
    {
        if (request.EnvId is null || request.EnvId == 0)
        {
            var entity = await _context.Envs
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }
        else
        {
            var entity = await _context.Envs
                            .Where(x => x.ProjectId == request.ProjectId
                                        && x.Id != request.EnvId
                                        && x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                            .SingleOrDefaultAsync(cancellationToken);
            if (entity is null)
            {
                return new Result<bool>(true);
            }
        }

        return new Result<bool>(false);
    }
}