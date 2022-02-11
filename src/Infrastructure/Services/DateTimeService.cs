using JobBet.Application.Common.Interfaces;

namespace JobBet.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}