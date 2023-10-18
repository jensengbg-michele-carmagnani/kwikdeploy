using KwikDeploy.Application.Common.Mappings;
using KwikDeploy.Domain.Entities;

namespace KwikDeploy.Application.AppDefs.Queries.AppDefGetList;

public class AppDefHeadDto : IMapFrom<Target>
{
    public int Id { get; init; }

    public string Name { get; init; } = null!;

    public string ImageName { get; init; } = null!;

    public string Tag { get; init; } = null!;
}
