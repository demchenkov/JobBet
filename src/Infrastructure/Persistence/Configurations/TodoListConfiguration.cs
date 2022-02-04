using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(2000)
            .IsRequired();
    }
}
