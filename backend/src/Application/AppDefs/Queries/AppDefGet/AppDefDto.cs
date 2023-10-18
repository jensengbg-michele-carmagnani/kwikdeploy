using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.AppDefs.Queries.AppDefGet;

public class AppDefDto : IMapFrom<AppDef>
{
    public int Id { get; init; }

    public int ProjectId { get; init; }

    public string Name { get; init; } = null!;

    public string ImageName { get; init; } = null!;

    public string Tag { get; init; } = null!;
}

