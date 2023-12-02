using RabbitMQ.Client;

namespace ServerStatisticsPublisher
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            await TopicPublisher.Publish(channel);
        }
    }
}