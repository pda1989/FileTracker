using Autofac;
using FileTracker.Common.Implementations;
using FileTracker.Common.Interfaces;
using FileTracker.Common.Models;
using System;

namespace FileTracker.Console
{
    internal class Program
    {
        private static IFilterManager _filterManager;
        private static IWatcherReportManager _reportManager;
        private static ISettings _settings;

        private static void Main(string[] args)
        {
            var bootstraper = new Bootstraper();
            bootstraper.InitAsync().Wait();

            _settings = Bootstraper.Container.Resolve<ISettings>();
            _filterManager = Bootstraper.Container.Resolve<IFilterManager>();
            _filterManager
                .AddFilter(new BaseStringFilter())
                .AddFilter(new RegexFilter(_settings.RegexFilter));
            _reportManager = Bootstraper.Container.Resolve<IWatcherReportManager>();

            using (var watcher = Bootstraper.Container.Resolve<IFileWatcher>())
            {
                watcher.OnFileChanged += OnFileChanged;
                watcher.WatchFiles(_settings.FilePath, _settings.FileMask);

                while (true)
                {
                    var command = System.Console.ReadLine();

                    if (command.Equals("exit", System.StringComparison.OrdinalIgnoreCase))
                        break;
                }
            }
        }

        private static async void OnFileChanged(object sender, FileWatcherEventArgs e)
        {
            try
            {
                if (_filterManager.IsMatch(e?.AddedContent))
                {
                    var report = new Report
                    {
                        FileName = e?.FileName,
                        AddedContent = e?.AddedContent,
                        Timestamp = System.DateTime.UtcNow
                    };
                    await _reportManager.SendReportAsync(report);
                }
            }
            catch (Exception exception)
            {
                System.Console.WriteLine(exception.Message);
            }
        }
    }
}