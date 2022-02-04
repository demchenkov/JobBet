namespace JobBet.Domain.Entities;

public class Skill
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public virtual ICollection<Freelancer> Freelancers { get; } = new List<Freelancer>();
}