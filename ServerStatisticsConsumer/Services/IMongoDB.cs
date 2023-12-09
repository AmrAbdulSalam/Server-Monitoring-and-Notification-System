
namespace ServerStatisticsConsumer.Services
{
    public interface IMongoDB
    {
        public Task Insert(ServerStatistics serverStatistics);
    }
}