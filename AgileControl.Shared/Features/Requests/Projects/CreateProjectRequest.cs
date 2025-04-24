namespace AgileControl.Shared.Features.Requests.Projects;

public class CreateProjectRequest
{
    public string Title { get; set; } = default!;

    public string? Description { get; set; }

    public DateTime? EndDate { get; set; }
}