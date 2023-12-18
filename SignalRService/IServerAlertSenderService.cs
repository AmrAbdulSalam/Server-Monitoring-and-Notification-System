namespace SignalRService
{
    public interface IServerAlertSenderService
    {
        public Task SendAlert(string alert);
    }
}