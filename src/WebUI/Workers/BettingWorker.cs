using JobBet.Application.Common.Configurations;
using JobBet.Domain.Entities;
using JobBet.Infrastructure.Persistence;
using JobBet.Infrastructure.QueuePackages;
using JobBet.WebUI.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace JobBet.WebUI.Workers;

public class BettingWorker : BackgroundService
{
    private readonly IHubContext<BettingHub> _bettingHub;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly QueueNames _queueNames;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BettingWorker(
        IHubContext<BettingHub> bettingHub,
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<QueueNames> queueNames, IServiceScopeFactory serviceScopeFactory)
    {
        _bettingHub = bettingHub;
        _connectionMultiplexer = connectionMultiplexer;
        _serviceScopeFactory = serviceScopeFactory;
        _queueNames = queueNames.Value ?? throw new ArgumentNullException(nameof(queueNames));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        ChannelMessageQueue? messageQueue = await _connectionMultiplexer.GetSubscriber()
            .SubscribeAsync(_queueNames.BettingQueue);
        messageQueue.OnMessage(EventHandler);
    }

    private async Task EventHandler(ChannelMessage channelMessage)
    {
        BetPackage? package = JsonConvert.DeserializeObject<BetPackage>(channelMessage.Message);

        if (package == null)
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        await using ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        Freelancer freelancer = await context.Freelancers.FirstAsync(x => x.Id == package.FreelancerId);

        await _bettingHub.Clients.Group(package.JobId.ToString())
            .SendAsync("newBetDetected", freelancer, package.Price);
    }
}