using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.AppDefs.Commands.AppDefUpdate;

public record AppDefUpdateCommand : IRequest
{
    [FromRoute]
    public int ProjectId { get; set; }

    [FromRoute]
    public int Id { get; set; }

    [FromBody]
    public AppDefUpdateCommandBody Body { get; init; } = null!;
}

public record AppDefUpdateCommandBody
{
    public string Name { get; init; } = null!;
    public string ImageName { get; init; } = null!;
    public string Tag { get; init; } = null!;
}


public class AppDefUpdateCommandHandler : IRequestHandler<AppDefUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public AppDefUpdateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AppDefUpdateCommand request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.AppDefs.FindAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(AppDef), request.Id);
        }

        // Project Existance Check
        var projectEntity = await _context.Projects.FindAsync(request.ProjectId, cancellationToken);
        if (projectEntity is null)
        {
            throw new NotFoundException(nameof(Project), request.ProjectId);
        }

        // Unique Name Check
        var existingEntity = await _context.AppDefs
                                .Where(x => x.ProjectId == request.ProjectId
                                            && x.Id != request.Id
                                            && x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another app definition with the same name already exists.")
            });
        }

        // Update
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Body.Name.Trim();
        entity.ImageName = request.Body.ImageName;
        entity.Tag = request.Body.Tag;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
