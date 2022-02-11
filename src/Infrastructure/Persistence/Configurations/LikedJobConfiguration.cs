using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class LikedJobConfiguration : IEntityTypeConfiguration<LikedJob>
{
    public void Configure(EntityTypeBuilder<LikedJob> builder)
    {
        builder.HasNoKey();

        builder.HasOne(x => x.Freelancer)
            .WithMany()
            .HasForeignKey(x => x.FreelancerId);

        builder.HasOne(x => x.Job)
            .WithMany()
            .HasForeignKey(x => x.JobId);
    }
}