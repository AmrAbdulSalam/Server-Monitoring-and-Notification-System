using System.Text.Json;
using ServerStatisticsService.Configurations;

namespace ServerStatisticsService
{
    public static class ReadConfigurations
    {
        public static ServerConfigurations? Configurations { get; set; }
        public static async Task ReadSettingsFile(string path)
        {
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                var jsonContent = await File.ReadAllTextAsync(path);
                Configurations = JsonSerializer.Deserialize<ServerConfigurations>(jsonContent);
            }
        }
    }
}