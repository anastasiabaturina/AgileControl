using AgileControl.Domain.Enums;

namespace AgileControl.Domain.Entities;

public class Task
{
    public Guid Id { get; set; }

    public string Titie { get; set; } = default!;

    public string? Description { get; set; }

    public string Type { get; set; }

    public DateTime CreatedDate { get; set; }

    public int? StoryPoint { get; set; }

    public DateTime Deadlines { get; set; }

    public Guid IdUserCreated { get; set; }

    public User UserCrested { get; set; } = default!;

    public ICollection<Guid>? ResponsebleUsers { get; set; }

    public ICollection<User>? ResponssebleUsers { get; set; }

    public ICollection<Tag>? Tags { get;set; } 

    public Priority Priority { get; set; }

    public Status Status { get; set; }
}