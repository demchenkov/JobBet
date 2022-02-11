using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using JobBet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobBet.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        IdentityRole administratorRole = new("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        ApplicationUser administrator = new() {UserName = "administrator@localhost", Email = "administrator@localhost"};

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] {administratorRole.Name});
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary

        if (!context.Clients.Any())
        {
            List<Client> clients = new()
            {
                new Client() {Title = "Test client", UserId = "3d1adb42-73f2-4218-94cb-d8a19c733b71"}
            };

            context.Clients.AddRange(clients);
            await context.SaveChangesAsync();
        }

        if (!context.Jobs.Any())
        {
            List<Job> jobs = new()
            {
                new Job()
                {
                    Title = "Test title 1",
                    Description = "Test description 1",
                    ExperienceLevel = ExperienceLevel.Entry,
                    JobType = JobType.OneTime,
                    Status = JobStatus.Created,
                    Client = context.Clients.First()
                },
                new Job()
                {
                    Title = "Test title 2",
                    Description = "Test description 2",
                    ExperienceLevel = ExperienceLevel.Entry,
                    JobType = JobType.OneTime,
                    Status = JobStatus.InProgress,
                    Client = context.Clients.First()
                },
                new Job()
                {
                    Title = "Test title 3",
                    Description = "Test description 3",
                    ExperienceLevel = ExperienceLevel.Entry,
                    JobType = JobType.OneTime,
                    Status = JobStatus.Done,
                    Client = context.Clients.First()
                }
            };

            context.Jobs.AddRange(jobs);

            await context.SaveChangesAsync();
        }

        if (!context.JobRatings.Any())
        {
            Job job = context.Jobs.First();
            List<JobRating> ratings = new()
            {
                new JobRating {Job = job, ClientScore = 1, FreelancerScore = 1},
                new JobRating {Job = job, ClientScore = 2, FreelancerScore = 2},
                new JobRating {Job = job, ClientScore = 3, FreelancerScore = 3},
                new JobRating {Job = job, ClientScore = 4, FreelancerScore = 4},
                new JobRating {Job = job, ClientScore = 5, FreelancerScore = 5}
            };

            context.JobRatings.AddRange(ratings);

            await context.SaveChangesAsync();
        }
    }
}