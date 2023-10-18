using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Commands.EnvCreate;

public record EnvCreateCommand : IRequest<Result<int>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromBody]
    public EnvCreateCommandBody Body { get; init; } = null!;
}

public record EnvCreateCommandBody
{
    public int TargetId { get; init; }

    public string Name { get; init; } = null!;
}

public class EnvCreateCommandHandler : IRequestHandler<EnvCreateCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public EnvCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(EnvCreateCommand request, CancellationToken cancellationToken)
    {
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
                                                && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                    .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity is not null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another environemnt with the same name already exists.")
            });
        }

        // Create
        var entity = new Env
        {
            ProjectId = request.ProjectId,
            TargetId = request.Body.TargetId,
            Name = request.Body.Name.Trim(),
        };
        _context.Envs.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
