using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Applicaion.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Project> Projects { get; set; } 

    public DbSet<ProjectMember> ProjectMembers { get; set; }

    public DbSet<ProjectTask> ProjectTasks { get; set; }

    public DbSet<Sprint> Sprints { get; set; }

    Task SaveChangesAsync(CancellationToken cancellationToken);
}