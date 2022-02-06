using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .IsRequired();
        
        builder.Property(t => t.Price)
            .HasColumnType("NUMERIC(10,5)")
            .IsRequired();

        builder.HasOne(t => t.Freelancer)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.FreelancerId);
    }
}
