using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;

namespace JobBet.Application.Projects.Queries.GetProjectById;

public class ProjectDetailsDto : IMapFrom<Project>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public ExperienceLevel ExperienceLevel { get; set; }
    
    public ProjectType? ProjectType { get; set; }
    
    public int? ExecutorId { get; set; }
}