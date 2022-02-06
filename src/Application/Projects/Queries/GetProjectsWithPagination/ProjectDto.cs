using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;

namespace JobBet.Application.Projects.Queries.GetProjectsWithPagination;

public class ProjectDto : IMapFrom<Project>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? ExperienceLevel { get; set; }
    
    public ProjectType? ProjectType { get; set; }
    
    public decimal Price { get; set; }

    public int? FreelancerId { get; set; }
}