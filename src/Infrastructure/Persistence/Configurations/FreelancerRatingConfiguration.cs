using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class FreelancerRatingConfiguration : IEntityTypeConfiguration<FreelancerRating>
{
    public void Configure(EntityTypeBuilder<FreelancerRating> builder)
    {
        builder.HasNoKey();

        // builder.HasOne(x => x.Freelancer)
        //     .WithOne(x => x.Rating)
        //     .HasForeignKey<FreelancerRating>(x => x.FreelancerId);

        builder.ToView("FreelancerRating_view");
    }
}