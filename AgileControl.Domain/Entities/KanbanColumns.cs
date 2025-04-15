namespace AgileControl.Domain.Entities;

public class KanbanColumns
{
    public Guid Id { get; set; }

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = default!;

    public string Title { get; set; } = default!;

    public ICollection<Task> Tasks {  get; set; } = new  List<Task>();
}
