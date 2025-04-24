namespace AgileControl.Shared.Features.Requests.Projects;

public class ProjectMemberRequest
{
    public Guid UserId { get; set; }

    public int? ProjectRole { get; set; }
}