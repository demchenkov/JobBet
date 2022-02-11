﻿using JobBet.Application.Common.Mappings;
using JobBet.Domain.Entities;
using JobBet.Domain.Enums;

namespace JobBet.Application.LikedJobs.Queries.GetMyLikedJobsWithPagination;

public class LikedJobDto : IMapFrom<Job>
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public int? ExperienceLevel { get; set; }

    public JobType? JobType { get; set; }

    public decimal Price { get; set; }

    public int? FreelancerId { get; set; }
}