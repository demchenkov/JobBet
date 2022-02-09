using JobBet.Domain.Entities;
using JobBet.Domain.Enums;
using JobBet.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace JobBet.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var administratorRole = new IdentityRole("Administrator");

        if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await roleManager.CreateAsync(administratorRole);
        }

        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await userManager.CreateAsync(administrator, "Administrator1!");
            await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }
    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        // Seed, if necessary
        
        if (!context.Clients.Any())
        {
            var clients = new List<Client>()
            {
                new Client {Title = "Test client", UserId = "3d1adb42-73f2-4218-94cb-d8a19c733b71"},
            };
            
            context.Clients.AddRange(clients);
            await context.SaveChangesAsync();
        }
        
        if (!context.Projects.Any())
        {
            var projects = new List<Project>()
            {
                new Project
                {
                    Title = "Test title 1",
                    Description = "Test description 1",
                    ExperienceLevel = ExperienceLevel.Entry,
                    ProjectType = ProjectType.OneTime,
                    Status = ProjectStatus.Created,
                    Client = context.Clients.First(), 
                },
                new Project
                {
                    Title = "Test title 2",
                    Description = "Test description 2",
                    ExperienceLevel = ExperienceLevel.Entry,
                    ProjectType = ProjectType.OneTime,
                    Status = ProjectStatus.InProgress,
                    Client = context.Clients.First(), 
                },
                new Project
                {
                    Title = "Test title 3",
                    Description = "Test description 3",
                    ExperienceLevel = ExperienceLevel.Entry,
                    ProjectType = ProjectType.OneTime,
                    Status = ProjectStatus.Done,
                    Client = context.Clients.First(), 
                },
            };
            
            context.Projects.AddRange(projects);

            await context.SaveChangesAsync();
        }

        if (!context.ProjectRatings.Any())
        {
            var project = context.Projects.First();
            var ratings = new List<ProjectRating>()
            {
                new()
                {
                    Project = project,
                    ClientScore = 1,
                    FreelancerScore = 1,
                },
                new()
                {
                    Project = project,
                    ClientScore = 2,
                    FreelancerScore = 2,
                },
                new()
                {
                    Project = project,
                    ClientScore = 3,
                    FreelancerScore = 3,
                },
                new()
                {
                    Project = project,
                    ClientScore = 4,
                    FreelancerScore = 4,
                },
                new()
                {
                    Project = project,
                    ClientScore = 5,
                    FreelancerScore = 5,
                },
            };
            
            context.ProjectRatings.AddRange(ratings);

            await context.SaveChangesAsync();
        }
    }
}
