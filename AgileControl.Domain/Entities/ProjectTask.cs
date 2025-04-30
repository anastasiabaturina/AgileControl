using AgileControl.Domain.Enums;

namespace AgileControl.Domain.Entities;

public class ProjectTask
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid IdUserCreated { get; set; }

    public User UserCreated { get; set; } = default!;

    public Guid AssigneeId { get; set; }

    public User Assignee { get; set; }

    public Project Project { get; set; }

    public Guid ProjectId { get; set; }

    public Priority Priority { get; set; }

    public ICollection<CheckList>? CheckList { get; set; }

    public ICollection<Comment>? Comments { get; set; }

    public Guid KanbanColumnId { get; set; }
    
    public KanbanColumn KanbanColumn { get; set; } = default!;
}