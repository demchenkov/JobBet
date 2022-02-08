using Microsoft.AspNetCore.SignalR;

namespace JobBet.WebUI.Hubs;

public class BettingHub : Hub
{
    public Task AddToGroup(string groupName)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public Task RemoveFromGroup(string groupName)
    {
        return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
    }

    
}