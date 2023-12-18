using ServerConfigurations.Configurations;
using ServerStatisticsService;
using SignalRService;

namespace ServerStatisticsConsumer
{
    public class AlertCalculations
    {
        private readonly AnomalyDetectionConfig _config;
        private ServerStatisticsDTO _serverStat;
        private ServerStatisticsDTO _prevServerSta = null;
        private ServerAlertSenderService _serverAlertService;
        public AlertCalculations(AnomalyDetectionConfig config  , ServerAlertSenderService serverAlertService)
        {
            _config = config;
            _serverAlertService = serverAlertService;
        }

        public async Task SendAlert(ServerStatisticsDTO serverStat)
        {
            _serverStat = serverStat;

            if (_prevServerSta == null)
            {
                _prevServerSta = _serverStat;
            }

            if (MemoryUsageIncreaseAnomaly())
                await _serverAlertService.SendAlert("Memory Usage Anomaly");
            if(CpuUsageIncreaseAnomaly())
                await _serverAlertService.SendAlert("Cpu Usage Anomaly");
            if(MemoryHighUsage())
                await _serverAlertService.SendAlert("Memory High Usage");
            if(CpuHighUsage())
                await _serverAlertService.SendAlert("Cpu High Usage");
        }

        private bool MemoryUsageIncreaseAnomaly()
        {
            var calThreshold = _prevServerSta.MemoryUsage * (1 + _config.MemoryUsageAnomalyThresholdPercentage);

            if (_serverStat.MemoryUsage > calThreshold)
                return true;

            return false;
        }

        private bool CpuUsageIncreaseAnomaly()
        {
            var calThreshold = _prevServerSta.CpuUsage * (1 + _config.CpuUsageAnomalyThresholdPercentage);

            if (_serverStat.CpuUsage > calThreshold)
                return true;

            return false;
        }

        private bool MemoryHighUsage()
        {
            var calThreshold = _serverStat.MemoryUsage / (_serverStat.MemoryUsage + _serverStat.AvailableMemory);

            if (calThreshold > _config.MemoryUsageThresholdPercentage)
                return true;

            return false;
        }

        private bool CpuHighUsage()
        {
            if (_serverStat.CpuUsage > _config.CpuUsageThresholdPercentage)
                return true;

            return false;
        }
    }
}