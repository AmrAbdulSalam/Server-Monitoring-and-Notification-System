using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRService
{
    public class ServerAlertSenderService : IServerAlertSenderService
    {
        private readonly HubConnection _hubConnection;
        public ServerAlertSenderService(string url)
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