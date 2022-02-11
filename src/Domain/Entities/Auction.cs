namespace JobBet.Domain.Entities;

public abstract class Auction<TKey, TLot>
    where TLot : class
    where TKey : IEquatable<TKey>

{
    public int Id { get; set; }

    public DateTimeOffset StartTime { get; set; }

    public DateTimeOffset FinishTime { get; set; }

    public decimal InitialPrice { get; set; }

    public TKey LotId { get; set; } = default!;

    public TLot Lot { get; set; } = default!;
}