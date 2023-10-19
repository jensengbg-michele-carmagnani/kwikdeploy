﻿using KwikDeploy.Application.Common.Exceptions;
using KwikDeploy.Application.Common.Interfaces;
using KwikDeploy.Application.Common.Models;
using KwikDeploy.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KwikDeploy.Application.Targets.Commands;

public record TargetRegenerateKey : IRequest<Result<string>>
{
    [FromRoute]
    public int ProjectId { get; init; }

    [FromRoute]
    public int Id { get; init; }
}

public class TargetRegenerateKeyHandler : IRequestHandler<TargetRegenerateKey, Result<string>>
{
    private readonly IApplicationDbContext _context;

    public TargetRegenerateKeyHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<string>> Handle(TargetRegenerateKey request, CancellationToken cancellationToken)
    {
        // Existance Check
        var entity = await _context.Targets
                        .Where(x => x.ProjectId == request.ProjectId && x.Id == request.Id)
                        .SingleOrDefaultAsync(cancellationToken);
        if (entity is null)
        {
            throw new NotFoundException(nameof(Target), request.Id);
        }

        // Update
        entity.Key = Guid.NewGuid().ToString();
        await _context.SaveChangesAsync(cancellationToken);

        return new Result<string>(entity.Key);
    }
}
