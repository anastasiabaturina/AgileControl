using AgileControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileControl.Infrastructure.Configuration;

public class ProjectMemberConfiguration : IEntityTypeConfiguration<ProjectMember>
{
    public void Configure(EntityTypeBuilder<ProjectMember> builder)
    {
        builder.HasKey(pm => new { pm.UserId, pm.PojectId });

        builder.HasOne(pm => pm.User)
            .WithMany()
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pm => pm.Project)
            .WithMany(p => p.ProgectMembers)
            .HasForeignKey(pm => pm.PojectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(pm => pm.ProjectRole)
           .HasConversion<int>();
    }
}