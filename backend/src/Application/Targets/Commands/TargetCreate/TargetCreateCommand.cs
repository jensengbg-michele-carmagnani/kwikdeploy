using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands.TargetCreate;

public record TargetCreateCommand : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public TargetCreateCommandBody Body { get; init; } = null!;
}

public record TargetCreateCommandBody
{
    public string Name { get; init; } = null!;
}

public class TargetCreateCommandHandler : IRequestHandler<TargetCreateCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public TargetCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(TargetCreateCommand request, CancellationToken cancellationToken)
    {
        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.Targets
                                    .Where(x => x.ProjectId == request.ProjectId
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another target with the same name already exists.")
            });
        }

        // Create
        var entity = new Target
        {
            ProjectId = request.ProjectId,
            Name = request.Body.Name.Trim(),
            Key = Guid.NewGuid().ToString(),
            ConnectionId = null
        };
        _context.Targets.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
