using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileControl.Infrastructure.Configuration;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Titie)
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

        builder.HasOne(t => t.UserCrested)
            .WithMany(u => u.CreatedTasks)
            .HasForeignKey(t => t.IdUserCreated)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(t => t.ResponsebleUsers)
            .WithMany()
            .UsingEntity(j => j.ToTable("TaskUsers"));

        builder.HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);

        builder.Property(pt => pt.Priority)
            .HasConversion<int>();

        builder.Property(pt => pt.Status)
            .HasConversion<int>();
    }
}