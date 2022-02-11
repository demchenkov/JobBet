using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class TalentAuctionConfiguration : IEntityTypeConfiguration<TalentAuction>
{
    public void Configure(EntityTypeBuilder<TalentAuction> builder)
    {
        builder.Property(x => x.InitialPrice)
            .HasPrecision(10, 2);
        
        builder.HasOne(x => x.Lot)
            .WithMany()
            .HasForeignKey(x => x.LotId);
    }
}