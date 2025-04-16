using AgileControl.Applicaion.Interfaces;
using AgileControl.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AgileControl.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Project> Projects { get; set; } 

    public DbSet<ProjectMember> ProjectMembers { get; set; } 

    public DbSet<ProjectTask> ProjectTasks { get; set; } 

    public DbSet<Sprint> Sprints { get; set; }
}