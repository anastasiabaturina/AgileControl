using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileControl.Infrastructure.Configuration;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(p => p.Description)
            .HasMaxLength(500)
            .HasDefaultValue(null);

        builder.HasOne(p => p.CreatorUser)
            .WithMany(p => p.CreatedProjects)
            .HasForeignKey(pt => pt.CreatorId);

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(pt => pt.ProjectId);

        builder.HasMany(p => p.ProgectMembers)
            .WithOne(pm => pm.Project)
            .HasForeignKey(pm => pm.PojectId);

        builder.Property(p => p.CreatedDate)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.EndDate)
            .IsRequired(false)
            .HasDefaultValue(null);
    }
}
