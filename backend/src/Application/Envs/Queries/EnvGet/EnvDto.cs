using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Envs.Queries.EnvGet;

public class EnvDto : IMapFrom<Env>
{
    public int Id { get; init; }

    public int ProjectId { get; init; }

    public int TargetId { get; init; }

    public string Name { get; init; } = null!;
}

