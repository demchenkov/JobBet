using JobBet.Domain.Entities;
using JobBet.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class FreelancerAverageRatingConfiguration : IEntityTypeConfiguration<FreelancerAverageRating>
{
    public void Configure(EntityTypeBuilder<FreelancerAverageRating> builder)
    {
        builder.HasKey(x => x.FreelancerId);
        builder.HasOne(x => x.Freelancer)
            .WithOne(x => x.Rating)
            .HasForeignKey<FreelancerAverageRating>(x => x.FreelancerId);

        builder.ToView(nameof(FreelancerAverageRating));
    }
}