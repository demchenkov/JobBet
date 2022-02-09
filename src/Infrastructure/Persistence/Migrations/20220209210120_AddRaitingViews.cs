using JobBet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBet.Infrastructure.Persistence.Migrations
{
    public partial class AddRaitingViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
create or alter view {nameof(ClientAverageRating)} as
select p.[ClientId], avg(cast(pr.ClientScore as float)) as [Score]
from [ProjectRatings] pr left join [Projects] p on pr.[ProjectId] = p.[Id]
where p.[ClientId] is not null and pr.[ClientScore] is not null
group by  p.[ClientId];
");
            
            migrationBuilder.Sql($@"
create or alter view {nameof(FreelancerAverageRating)} as
select p.[ExecutorId], avg(cast(pr.FreelancerScore as float)) as [Score]
from [ProjectRatings] pr left join [Projects] p on pr.[ProjectId] = p.[Id]
where p.[ExecutorId] is not null and pr.[FreelancerScore] is not null
group by  p.[ExecutorId];
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"drop view {nameof(FreelancerAverageRating)};");
            migrationBuilder.Sql($@"drop view {nameof(ClientAverageRating)};");
        }
    }
}
