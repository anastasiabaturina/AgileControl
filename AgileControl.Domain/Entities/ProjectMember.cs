using AgileControl.Domain.Enums;

namespace AgileControl.Domain.Entities;

public class ProjectMember
{
    public Guid UserId { get; set; }

    public Guid PojectId { get; set; }

    public User User { get; set; } = default!;

    public Project Project { get; set; } = default!;

    public ProjectRole? ProjectRole { get; set; }
}