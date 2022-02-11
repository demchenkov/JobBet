namespace JobBet.Domain.Entities;

public class LanguageSkill
{
    public int Id { get; set; }

    public LanguageProficiencyLevel Level { get; set; }

    public int LanguageId { get; set; }
    public Language Language { get; set; } = default!;

    public virtual ICollection<Freelancer> Freelancers { get; } = new List<Freelancer>();
}