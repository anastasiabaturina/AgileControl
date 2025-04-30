namespace AgileControl.Domain.Entities;

public class KanbanColumn
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public ICollection<ProjectTask>? Tasks { get; set; } = new List<ProjectTask>();
}