namespace AgileControl.Domain.Entities;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime EndDate { get; set; }

    public ICollection<Task> Tasks { get; set; } = new List<Task>();

    public ICollection<ProjectMember> ProgectMembers { get; set; } = new List<ProjectMember>();
}