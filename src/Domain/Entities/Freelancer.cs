namespace JobBet.Domain.Entities;

public class Freelancer
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? AvatarUrl { get; set; }

    public string UserId { get; set; } = default!;

    public List<Skill> Skills { get; } = new();

    public List<LanguageSkill> LanguageSkills { get; set; } = new();

    public List<Job> Jobs { get; } = new();

    public FreelancerAverageRating? Rating { get; set; }
}