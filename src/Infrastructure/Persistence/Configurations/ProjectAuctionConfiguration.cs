using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class ProjectAuctionConfiguration : IEntityTypeConfiguration<ProjectAuction>
{
    public void Configure(EntityTypeBuilder<ProjectAuction> builder)
    {
        builder.Property(x => x.InitialPrice)
            .HasPrecision(10, 2);
        
        builder.HasOne(x => x.Lot)
            .WithOne(x => x.Auction)
            .HasForeignKey<ProjectAuction>(x => x.LotId);
    }
}