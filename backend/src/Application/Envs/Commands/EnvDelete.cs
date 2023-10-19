﻿using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Envs.Commands;

public record EnvDelete() : IRequest
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class EnvDeleteHandler : IRequestHandler<EnvDelete>
{
    private readonly IApplicationDbContext _context;

    public EnvDeleteHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EnvDelete request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Envs
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Delete
        _context.Envs.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
