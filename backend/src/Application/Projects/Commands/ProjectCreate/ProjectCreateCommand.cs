using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Commands.ProjectCreate;

public record ProjectCreateCommand : IRequest<Result<int>>
{
    [FromBody]
    public ProjectCreateCommandBody Body { get; init; } = null!;
}

public record ProjectCreateCommandBody
{
    public string Name { get; init; } = null!;
}

public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public ProjectCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
    {
        // Unique Name Check
        var existingEntity = await _context.Projects
                                .Where(x => x.Name.Trim().ToLower() == request.Body.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Body.Name), "Another project with the same name already exists.")
            });
        }

        // Create
        var entity = new Project
        {
            Name = request.Body.Name.Trim()
        };
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<int>(entity.Id);
    }
}
