using AgileControl.Domain.Enums;

namespace AgileControl.Domain.Entities;

public class ProjectTask
{
    public Guid Id { get; set; }

    public string Titie { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? StoryPoint { get; set; }

    public DateTime EndDate { get; set; }

    public Guid IdUserCreated { get; set; }

    public User UserCrested { get; set; } = default!;

    public ICollection<User>? ResponsebleUsers { get; set; }

    public Project Project { get; set; }

    public Guid ProjectId { get; set; }

    public Priority Priority { get; set; }

    public Enums.TaskStatus Status { get; set; }
}