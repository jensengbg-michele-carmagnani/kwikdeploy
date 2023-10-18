using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.Envs.Queries.EnvGetList;

public class EnvHeadDto : IMapFrom<Env>
{
    public int Id { get; init; }

    public string TargetName { get; init; } = null!;

    public string Name { get; init; } = null!;
}

