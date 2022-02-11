using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using JobBet.Application.Common.Interfaces;
using JobBet.Domain.Common;
using JobBet.Domain.Entities;
using JobBet.Domain.ValueObjects;
using JobBet.Infrastructure.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace JobBet.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTime _dateTime;
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        ICurrentUserService currentUserService,
        IDomainEventService domainEventService,
        IDateTime dateTime) : base(options, operationalStoreOptions)
    {
        _currentUserService = currentUserService;
        _domainEventService = domainEventService;
        _dateTime = dateTime;
    }

    public DbSet<ClientAverageRating> ClientAverageRatings => Set<ClientAverageRating>();
    public DbSet<FreelancerAverageRating> FreelancerAverageRatings => Set<FreelancerAverageRating>();

    public DbSet<Language> Languages => Set<Language>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<LanguageSkill> LanguageSkills => Set<LanguageSkill>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<Freelancer> Freelancers => Set<Freelancer>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<JobRating> JobRatings => Set<JobRating>();
    public DbSet<JobAuction> JobAuctions => Set<JobAuction>();
    public DbSet<LikedJob> LikedJobs => Set<LikedJob>();
    public DbSet<TalentAuction> TalentAuctions => Set<TalentAuction>();


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        DomainEvent[] events = ChangeTracker.Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished)
            .ToArray();

        int result = await base.SaveChangesAsync(cancellationToken);

        await DispatchEvents(events);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private async Task DispatchEvents(DomainEvent[] events)
    {
        foreach (DomainEvent @event in events)
        {
            @event.IsPublished = true;
            await _domainEventService.Publish(@event);
        }
    }
}