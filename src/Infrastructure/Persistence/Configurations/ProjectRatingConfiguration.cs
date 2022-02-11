using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class ProjectRatingConfiguration : IEntityTypeConfiguration<ProjectRating>
{
    public void Configure(EntityTypeBuilder<ProjectRating> builder)
    {
        builder.HasOne(x => x.Project)
            .WithOne()
            .HasForeignKey<ProjectRating>(x => x.ProjectId);

        builder.HasIndex(x => x.ProjectId);
    }
}