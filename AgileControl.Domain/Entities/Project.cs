namespace AgileControl.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }

    public Guid CreatorId { get; set; }

    public User CreatorUser{ get; set; } = default!;

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EndDate { get; set; }

    public ICollection<ProjectTask>? Tasks { get; set; }

    public ICollection<ProjectMember>? ProgectMembers { get; set; }

    public ICollection<Sprint>? Sprints { get; set; }
}