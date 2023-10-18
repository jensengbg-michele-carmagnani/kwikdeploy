namespace KwikDeploy.Domain.Entities;

public class Target : BaseAuditableEntity
{
    public int ProjectId { get; set; }

    public Project Project { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string? ConnectionId { get; set; } = null!;
}
