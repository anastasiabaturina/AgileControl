using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileControl.Infrastructure.Configuration;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .HasDefaultValue(null);

        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.EndDate)
           .IsRequired(false)
           .HasDefaultValue(null);

        builder.Property(t => t.StoryPoint)
            .IsRequired(false)
            .HasDefaultValue(0);

        builder.HasOne(t => t.UserCreated)
            .WithMany(u => u.CreatedTasks)
            .HasForeignKey(t => t.IdUserCreated);

        builder.HasMany(t => t.ResponsebleUsers)
            .WithMany(u => u.AssignedTasks)
            .UsingEntity<Dictionary<string, object>>(
                "TaskUsers",
                j => j
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne<ProjectTask>()
                    .WithMany()
                    .HasForeignKey("ProjectTaskId")
                    .OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("UserId", "ProjectTaskId");
                });

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);

        builder.Property(pt => pt.Priority)
            .HasConversion<int>();

        builder.Property(pt => pt.Status)
            .HasConversion<int>();
    }
}