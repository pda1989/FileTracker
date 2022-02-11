using FileTracker.Common.Interfaces;
using Newtonsoft.Json;

namespace FileTracker.Common.Implementations
{
    internal class JsonSettingsProvider : ISettingsProvider
    {
        private const string SettingsFileName = "service.config.json";

        private readonly IFile _fileIo;
        private readonly ILogger _logger;
        private readonly Settings _settings;

        public JsonSettingsProvider(Settings settings, IFile fileIo, ILogger logger)
        {
            _settings = settings;
            _fileIo = fileIo;
            _logger = logger;
        }

        public void InitSettings()
        {
            _logger.Info("Init settings");
            _logger.Debug($"  Settings file name: '{SettingsFileName}'");

            var text = _fileIo.ReadAllText(SettingsFileName);
            if (string.IsNullOrEmpty(text))
            {
                _logger.Error("Setting file is empty");
                return;
            }

            var settings = JsonConvert.DeserializeObject<Settings>(text);

            foreach (var property in settings.GetType().GetProperties())
            {
                var value = property.GetValue(settings);
                property.SetValue(_settings, value);
                _logger.Debug($"  Property: '{property.Name}' Value: '{value}'");
            }
        }
    }
}