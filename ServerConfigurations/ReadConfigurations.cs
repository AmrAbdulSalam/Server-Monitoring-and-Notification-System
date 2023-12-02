using System.Text.Json;

namespace Configurations
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