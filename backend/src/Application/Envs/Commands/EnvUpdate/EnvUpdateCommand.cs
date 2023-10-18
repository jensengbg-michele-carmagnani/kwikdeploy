using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Commands.EnvUpdate;

public record EnvUpdateCommand : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }

    [FromBody]
    public EnvUpdateCommandBody Body { get; init; } = null!;
}

public record EnvUpdateCommandBody
{
    public int TargetId { get; init; }

    public string Name { get; init; } = null!;
}

public class EnvUpdateCommandHandler : IRequestHandler<EnvUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public EnvUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EnvUpdateCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Envs.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Target Existance Check
        var targetEntity = await _context.Targets.FindAsync(request.Body.TargetId, cancellationToken);
        if (targetEntity is null)
        {
            throw new NotFoundException(nameof(Target), request.Body.TargetId);
        }

        // Unique Name Check
        var existingEntity = await _context.Envs
                                .Where(x => x.ProjectId == request.ProjectId
                                            && x.Id != request.Id
                                            && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another environment with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.TargetId = request.Body.TargetId;
        entity.Name = request.Body.Name.Trim();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
