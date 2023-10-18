namespace KwikDeploy.Domain.Entities;

public class AppDef : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ImageName { get; set; } = null!;

    public string Tag { get; set; } = null!;
}
