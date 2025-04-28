namespace AgileControl.Domain.Entities;

public class CheckList
{
    public Guid Id { get; set; }

    public Guid TaskId { get; set; }

    public ProjectTask Task { get; set; } = default!;

    public string Text { get; set; } = default!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }
}