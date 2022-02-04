using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobBet.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }

    DbSet<Freelancer> Freelancers { get; }
    
    DbSet<Language> Languages { get; }
    
    DbSet<Skill> Skills { get; }
    
    DbSet<LanguageSkill> LanguageSkills { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
