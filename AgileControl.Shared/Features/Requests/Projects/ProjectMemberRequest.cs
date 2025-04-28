namespace AgileControl.Shared.Features.Requests.Projects;

public class ProjectMemberRequest
{
    public Guid UserId { get; set; }

    public ProjectRole? ProjectRole { get; set; }

    public string? UserName { get; set; }
    public string? Email { get; set; }
}