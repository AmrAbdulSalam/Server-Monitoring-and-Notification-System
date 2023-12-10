namespace SignalRService
{
    public interface IServerAlertService
    {
        public Task SendAlert(string alert);
    }
}