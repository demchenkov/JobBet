namespace JobBet.Domain.Entities;

public class Freelancer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }

    public List<Skill> Skills { get; } = new List<Skill>();

    public List<LanguageSkill> LanguageSkills { get; set; } = new List<LanguageSkill>();

    public List<Project> Projects { get; } = new List<Project>();

    public string UserId { get; set; } = default!;
}