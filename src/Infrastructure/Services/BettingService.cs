using System.Globalization;
using JobBet.Application.Common.Configurations;
using JobBet.Application.Common.Interfaces;
using JobBet.Infrastructure.QueuePackages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace JobBet.Infrastructure.Services;

public class BettingService : IBettingService
{
    private const string BettingPrefix = "betting:project";
    private readonly IConnectionMultiplexer _connection;
    private readonly QueueNames _queueNames;

    public BettingService(IConnectionMultiplexer connection, IOptions<QueueNames> queueNames)
    {
        _connection = connection;
        _queueNames = queueNames.Value ?? throw new ArgumentNullException(nameof(queueNames));
    }

    public async Task MakeBetAsync(int freelancerId, int jobId, decimal price)
    {
        var db = _connection.GetDatabase();
        
        var hashSetKey = $"{BettingPrefix}#{jobId}";
        var bestJobPriceKey = $"{hashSetKey}:best";
        var formattedPrice = price.ToString(CultureInfo.InvariantCulture);
        
        await db.HashSetAsync(hashSetKey, $"{freelancerId}", formattedPrice);
        
        string bestPriceStr = await db.StringGetAsync(bestJobPriceKey);
        decimal bestPrice = decimal.TryParse(bestPriceStr, out bestPrice) ? bestPrice : decimal.MaxValue;
        
        if (bestPrice > price)
        {
            var package = new BetPackage
            {
                Price = price,
                FreelancerId = freelancerId,
                JobId = jobId,
            };
            await db.StringSetAsync(bestJobPriceKey, formattedPrice);
            await db.PublishAsync(_queueNames.BettingQueue, JsonConvert.SerializeObject(package)); // TODO: think about protobuf or smth like that
        }
    }
}