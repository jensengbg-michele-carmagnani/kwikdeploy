using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.Envs.Queries.EnvGetList;

public record EnvGetList : IRequest<PaginatedList<EnvHeadDto>>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class EnvGetListHandler : IRequestHandler<EnvGetList, PaginatedList<EnvHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public EnvGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<EnvHeadDto>> Handle(EnvGetList request, CancellationToken cancellationToken)
    {
        return await (from e in _context.Envs
                      join t in _context.Targets on e.TargetId equals t.Id
                      where e.ProjectId == request.ProjectId
                      orderby e.Name
                      select new EnvHeadDto
                      {
                          Id = e.Id,
                          TargetName = t.Name,
                          Name = e.Name,
                      })
                     .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}

