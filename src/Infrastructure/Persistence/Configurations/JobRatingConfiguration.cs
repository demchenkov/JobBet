using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class JobRatingConfiguration : IEntityTypeConfiguration<JobRating>
{
    public void Configure(EntityTypeBuilder<JobRating> builder)
    {
        builder.HasOne(x => x.Job)
            .WithOne()
            .HasForeignKey<JobRating>(x => x.JobId);

        builder.HasIndex(x => x.JobId);
    }
}