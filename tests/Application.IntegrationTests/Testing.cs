using JobBet.Application.Common.Interfaces;
using JobBet.Infrastructure.Identity;
using JobBet.Infrastructure.Persistence;
using JobBet.WebUI;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;

namespace JobBet.Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static IConfigurationRoot _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static Checkpoint _checkpoint = null!;
    private static string? _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        IConfigurationBuilder? builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

        _configuration = builder.Build();

        Startup startup = new Startup(_configuration);

        ServiceCollection services = new ServiceCollection();

        services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
            w.EnvironmentName == "Development" &&
            w.ApplicationName == "JobBet.WebUI"));

        services.AddLogging();

        startup.ConfigureServices(services);

        // Replace service registration for ICurrentUserService
        // Remove existing registration
        ServiceDescriptor? currentUserServiceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(ICurrentUserService));

        if (currentUserServiceDescriptor != null)
        {
            services.Remove(currentUserServiceDescriptor);
        }

        // Register testing version
        services.AddTransient(provider =>
            Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

        _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

        _checkpoint = new Checkpoint {TablesToIgnore = new[] {"__EFMigrationsHistory"}};

        EnsureDatabase();
    }

    private static void EnsureDatabase()
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.Migrate();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task<string> RunAsDefaultUserAsync()
    {
        return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
    }

    public static async Task<string> RunAsAdministratorAsync()
    {
        return await RunAsUserAsync("administrator@local", "Administrator1234!", new[] {"Administrator"});
    }

    public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        UserManager<ApplicationUser> userManager =
            scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        ApplicationUser user = new ApplicationUser {UserName = userName, Email = userName};

        IdentityResult? result = await userManager.CreateAsync(user, password);

        if (roles.Any())
        {
            RoleManager<IdentityRole> roleManager =
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _currentUserId = user.Id;

            return _currentUserId;
        }

        string errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    public static async Task ResetState()
    {
        await _checkpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));

        _currentUserId = null;
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
    }
}