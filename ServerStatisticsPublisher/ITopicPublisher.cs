using ServerStatisticsService;

namespace ServerStatisticsPublisher
{
    public interface ITopicPublisher
    {
        void Publish(ServerStatisticsDTO message, string serverIdentifier);
    }
}