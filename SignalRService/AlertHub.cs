using Microsoft.AspNetCore.SignalR;

public class AlertHub : Hub
{
    public async Task SendAlertAsync(string message)
    {
        await Clients.All.SendAsync("Alert",message);
    }
}