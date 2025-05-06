using AgileControl.Domain.Enums;

namespace AgileControl.Shared.Features.Requests.Tasks;

public class CreateTaskRequest
{
    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }

    public Guid? AssigneeId { get; set; }

    public Guid ProjectId { get; set; }

    public Priority Priority { get; set; }

    public Guid? ColumnId { get; set; }
}