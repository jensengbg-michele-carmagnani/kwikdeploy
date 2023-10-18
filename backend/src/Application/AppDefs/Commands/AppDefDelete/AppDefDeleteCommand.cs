using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Commands.AppDefDelete;

public record AppDefDeleteCommand : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class AppDefDeleteCommandHandler : IRequestHandler<AppDefDeleteCommand>
{
    private readonly IApplicationDbContext _context;

    public AppDefDeleteCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AppDefDeleteCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.AppDefs
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.AppDefs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

