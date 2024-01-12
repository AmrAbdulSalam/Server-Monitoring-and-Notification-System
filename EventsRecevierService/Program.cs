using SignalRService;
using Configurations;

string? path = @"D:\intership\ServerMonitoringAndNotificationSystem\appsettings.json";
await ReadConfigurations.ReadSettingsFile(path);

var eventRecevier = new ServerAlertConsumerService(ReadConfigurations.Configurations.SignalRConfig.SignalRUrl);

Console.WriteLine("Events listner .... ");

await eventRecevier.ReceiveEvents();

Console.ReadLine();