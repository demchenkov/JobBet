using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class ClientAverageRatingConfiguration : IEntityTypeConfiguration<ClientAverageRating>
{
    public void Configure(EntityTypeBuilder<ClientAverageRating> builder)
    {
        builder.HasKey(x => x.ClientId);
        builder.HasOne(x => x.Client)
            .WithOne(x => x.Rating)
            .HasForeignKey<ClientAverageRating>(x => x.ClientId);

        builder.ToView(nameof(ClientAverageRating));
    }
}