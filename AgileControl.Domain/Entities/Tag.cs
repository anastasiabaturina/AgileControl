namespace AgileControl.Domain.Entities;

public class Tag
{
    public Guid Id { get;set; }

    public string Title { get; set; } = default!;

    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
