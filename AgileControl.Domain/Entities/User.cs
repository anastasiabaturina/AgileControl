using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace AgileControl.Domain.Entities;

public class User : IdentityUser<Guid>
{
    [JsonIgnore]
    public ICollection<ProjectMember> ProjectMemberships { get; set; } = new List<ProjectMember>();

    [JsonIgnore]
    public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();

    [JsonIgnore]
    public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();

    [JsonIgnore]
    public ICollection<ProjectTask> CreatedTasks { get; set; } = new List<ProjectTask>();
}