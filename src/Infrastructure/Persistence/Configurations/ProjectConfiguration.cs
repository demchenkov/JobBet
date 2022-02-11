using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Ignore(e => e.DomainEvents);
        
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .IsRequired();

        builder.HasOne(t => t.Executor)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.ExecutorId);

        builder.HasOne(t => t.Client)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.ClientId);
    }
}
