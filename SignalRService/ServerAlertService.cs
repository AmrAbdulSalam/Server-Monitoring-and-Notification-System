using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRService
{
    public class ServerAlertService : IServerAlertService
    {
        private readonly HubConnection _hubConnection;
        public ServerAlertService(string url)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }

        public async Task SendAlert(string alert)
        {
            await _hubConnection.StartAsync();

            await _hubConnection.InvokeAsync<string>("SendAlertAsync", alert);

            await _hubConnection.StopAsync();
        }
    }
}