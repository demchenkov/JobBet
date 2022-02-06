using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobBet.Infrastructure.Persistence.Configurations;

public class LanguageSkillConfiguration : IEntityTypeConfiguration<LanguageSkill>
{
    public void Configure(EntityTypeBuilder<LanguageSkill> builder)
    {
    }
}