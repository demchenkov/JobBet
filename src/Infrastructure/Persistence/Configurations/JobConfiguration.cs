using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasOne(t => t.Executor)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.ExecutorId);

        builder.HasOne(t => t.Client)
            .WithMany(x => x.Jobs)
            .HasForeignKey(x => x.ClientId);
    }
}