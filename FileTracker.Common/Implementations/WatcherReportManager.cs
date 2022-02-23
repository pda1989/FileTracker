using FileTracker.Common.Interfaces;
using FileTracker.Common.Interfaces.REST;
using FileTracker.Common.Models;
using System.Threading.Tasks;

namespace FileTracker.Common.Implementations
{
    public class WatcherReportManager : IWatcherReportManager
    {
        private readonly IApiClient _apiClient;
        private readonly ILogger _logger;
        private readonly ISettings _settings;

        public WatcherReportManager(IApiClient apiClient, ILogger logger, ISettings settings)
        {
            _apiClient = apiClient;
            _logger = logger;
            _settings = settings;
        }

        public async Task SendReportAsync(Report report)
        {
            _logger.Info("Send a report");

            var result = await _apiClient.PostAsync(
                _settings.EndpointUrl,
                report,
                _settings.RequestTimeoutMiliseconds);

            if (result?.IsSuccesfull == true)
            {
                _logger.Info("The report has been sent successfully");
            }
            else
            {
                _logger.Error($"Error: '{result?.Error?.Message}'");
            }
        }
    }
}