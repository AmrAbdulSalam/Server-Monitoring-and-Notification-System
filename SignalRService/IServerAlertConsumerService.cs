namespace SignalRService
{
    public interface IServerAlertConsumerService
    {
        Task ReceiveEvents();
    }
}