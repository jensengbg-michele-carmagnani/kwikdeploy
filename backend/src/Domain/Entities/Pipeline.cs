namespace KwikDeploy.Domain.Entities;

public class Pipeline : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;
}
