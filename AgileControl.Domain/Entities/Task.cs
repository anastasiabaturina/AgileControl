using AgileControl.Domain.Enums;

namespace AgileControl.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }

    public Guid IdUserCreated { get; set; }

    public IList<Guid> ResponsebleUsers { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public string Type { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int StoryPoint { get; set; }

    public DateTime Deadlines { get; set; }

    public Priority Priority { get; set; }

    public Status Status { get; set; }
}
