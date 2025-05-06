using AgileControl.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AgileControl.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; } 

    public DbSet<ProjectMember> ProjectMembers { get; set; } 

    public DbSet<ProjectTask> ProjectTasks { get; set; } 

    public DbSet<KanbanColumn> Columns { get; set; }

    public DbSet<CheckList> CheckLists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<User>(entity =>
        {
            entity.Ignore(u => u.UserName);  
            entity.Property(u => u.Email).IsRequired(); 
        });
    }
}