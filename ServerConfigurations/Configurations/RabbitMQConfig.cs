
namespace ServerConfigurations.Configurations
{
    public class RabbitMQConfig
    {
        public string? RabbitMQUrl { get; set; }
        public string? Exchange { get; set; }
        public string? Queue { get; set; }
        public string? RoutingKey { get; set; }
    }
}