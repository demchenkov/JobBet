using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class FreelancerConfiguration : IEntityTypeConfiguration<Freelancer>
{
    public void Configure(EntityTypeBuilder<Freelancer> builder)
    {
        // builder.Ignore(e => e.DomainEvents);

        builder.Property(t => t.FirstName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.LastName)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .IsRequired();
        
        builder.Property(t => t.AvatarUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasMany(t => t.Skills)
            .WithMany(t => t.Freelancers);

        builder.HasMany(t => t.LanguageSkills)
            .WithMany(t => t.Freelancers);

        builder.HasMany(x => x.Projects)
            .WithOne();
    }
}
