﻿using JobBet.Application.Common.Configurations;
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
    private readonly ILogger<BettingWorker> _logger;
    private readonly IHubContext<BettingHub> _clockHub;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly QueueNames _queueNames;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public BettingWorker(
        ILogger<BettingWorker> logger, 
        IHubContext<BettingHub> clockHub, 
        IConnectionMultiplexer connectionMultiplexer, 
        IOptions<QueueNames> queueNames, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _clockHub = clockHub;
        _connectionMultiplexer = connectionMultiplexer;
        _serviceScopeFactory = serviceScopeFactory;
        _queueNames = queueNames.Value ?? throw new ArgumentNullException(nameof(queueNames));
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var messageQueue = await _connectionMultiplexer.GetSubscriber()
            .SubscribeAsync(_queueNames.BettingQueue);
        messageQueue.OnMessage(EventHandler);
    }

    private async Task EventHandler(ChannelMessage channelMessage)
    {
        var package = JsonConvert.DeserializeObject<BetPackage>(channelMessage.Message);
        
        if (package == null)
        {
            return;
        }
        
        using var scope = _serviceScopeFactory.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var freelancer = await context.Freelancers.FirstAsync(x => x.Id == package.FreelancerId);
            
        await _clockHub.Clients.Group(package.ProjectId.ToString()).SendAsync("newBetDetected", freelancer, package.Price);
    }
}