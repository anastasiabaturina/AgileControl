using Microsoft.AspNetCore.Identity;

namespace AgileControl.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();

    public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();

    public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();

    public ICollection<ProjectTask> CreatedTasks { get; set; } = new List<ProjectTask>();
}