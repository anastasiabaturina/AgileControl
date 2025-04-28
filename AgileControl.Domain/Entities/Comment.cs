namespace AgileControl.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }

    public string Content { get; set; }

    public Guid TaskId { get; set; }

    public ProjectTask ProjectTask { get; set; }

    public Guid AuthorId { get; set; }

    public User Author { get; set; }

    public Guid? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }

    public DateTime CreatedAt { get; set; }
}