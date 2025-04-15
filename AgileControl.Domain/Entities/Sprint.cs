namespace AgileControl.Domain.Entities;

public class Sprint
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = default!;

    public DateTime StartDate { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsActive { get; set; }
}
