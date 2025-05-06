namespace AgileControl.Shared.Features.Requests.Columns;

public class CreateColumnRequest
{
    public string Title { get; set; } = default!;

    public Guid ProjectId { get; set; }
}
