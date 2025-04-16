using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileControl.Infrastructure.Configuration;

public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
{
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title)
            .HasMaxLength(255);

        builder.HasMany(s => s.Tasks)
            .WithMany()
            .UsingEntity(j => j.ToTable("SprintsTasks"));

        builder.HasOne(s => s.Project)
            .WithMany(p => p.Sprints)
            .HasForeignKey(s => s.ProjectId);

        builder.Property(p => p.StartDate)
           .IsRequired(false)
           .HasDefaultValue(null);

        builder.Property(p => p.EndDate)
           .IsRequired(false)
           .HasDefaultValue(null);
    }
}
