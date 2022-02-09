﻿namespace JobBet.Domain.Entities;

public class ClientAverageRating
{
    public double Score { get; set; }
    
    public int ClientId { get; set; }
    public Client Client { get; set; } = default!;
}