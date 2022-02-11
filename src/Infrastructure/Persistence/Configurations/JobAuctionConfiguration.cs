using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class JobAuctionConfiguration : IEntityTypeConfiguration<JobAuction>
{
    public void Configure(EntityTypeBuilder<JobAuction> builder)
    {
        builder.Property(x => x.InitialPrice)
            .HasPrecision(10, 2);

        builder.HasOne(x => x.Lot)
            .WithOne(x => x.Auction)
            .HasForeignKey<JobAuction>(x => x.LotId);
    }
}