using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KwikDeploy.Application.AppDefs.Queries.AppDefGetList;

public record AppDefGetList : IRequest<PaginatedList<AppDefHeadDto>>
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromQuery]
    public int PageNumber { get; init; } = 1;

    [FromQuery]
    public int PageSize { get; init; } = 10;
}

public class AppDefGetListHandler : IRequestHandler<AppDefGetList, PaginatedList<AppDefHeadDto>>
{
    private readonly IApplicationDbContext _context;

    public AppDefGetListHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<AppDefHeadDto>> Handle(AppDefGetList request, CancellationToken cancellationToken)
    {
        return await _context.AppDefs
            .Where(x => x.ProjectId == request.ProjectId)
            .OrderBy(x => x.Name)
            .Select(x => new AppDefHeadDto
            {
                Id = x.Id,
                Name = x.Name,
                ImageName = x.ImageName,
                Tag =x.Tag,
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
