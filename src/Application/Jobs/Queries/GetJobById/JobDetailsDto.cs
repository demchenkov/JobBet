using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;

namespace JobBet.Application.Jobs.Queries.GetJobById;

public class JobDetailsDto : IMapFrom<Job>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }
    
    public JobType? JobType { get; set; }
    
    public int? ExecutorId { get; set; }
}