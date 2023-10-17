﻿using FluentValidation.Results;
using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Projects.Commands.ProjectCreate;

public record ProjectCreateCommand : IRequest<int>
{
    public string Name { get; init; } = null!;
}

public class ProjectCreateCommandHandler : IRequestHandler<ProjectCreateCommand, int>
{
    private readonly IApplicationDbContext _context;

    public ProjectCreateCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(ProjectCreateCommand request, CancellationToken cancellationToken)
    {
        // Unique Name Check
        var existingEntity = await _context.Projects
                                .Where(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower())
                                .SingleOrDefaultAsync(cancellationToken);
        if (existingEntity != null)
        {
            throw new ValidationException(new List<ValidationFailure> {
                new ValidationFailure(nameof(request.Name), "Another project with the same name already exists.")
            });
        }

        // Create
        var entity = new Project
        {
            Name = request.Name.Trim()
        };
        _context.Projects.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
