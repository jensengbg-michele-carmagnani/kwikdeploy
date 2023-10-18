namespace KwikDeploy.Domain.Entities;

public class Env : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public int TargetId { get; set; }

    public Target Target { get; set; } = null!;

    public string Name { get; set; } = null!;
}
