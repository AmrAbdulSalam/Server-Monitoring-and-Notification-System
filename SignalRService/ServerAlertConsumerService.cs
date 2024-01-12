using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRService
{
    public class ServerAlertConsumerService : IServerAlertConsumerService
    {
        private readonly HubConnection _hubConnection;

        public ServerAlertConsumerService(string url)
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();
        }

        public async Task ReceiveEvents()
        {
            _hubConnection.On<string>("Alert", alert =>
            {
                Console.WriteLine(alert);
            });
            await _hubConnection.StartAsync();
        }
    }
}