using Autofac;
using FileTracker.Common.Implementations;
using FileTracker.Common.Interfaces;
using FileTracker.Common.Models;

namespace FileTracker.Console
{
    internal class Program
    {
        private static IFilterManager _filterManager;
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

            using (var watcher = Bootstraper.Container.Resolve<IFileWatcher>())
            {
                watcher.OnFileChanged += OnFileChanged;

                while (true)
                {
                    var command = System.Console.ReadLine();

                    if (command.Equals("exit", System.StringComparison.OrdinalIgnoreCase))
                        break;

                    if (command.Equals("start", System.StringComparison.OrdinalIgnoreCase))
                        watcher.WatchFiles(_settings.FilePath, _settings.FileMask);

                    if (command.Equals("stop", System.StringComparison.OrdinalIgnoreCase))
                        watcher.StopWatching();
                }
            }
        }

        private static void OnFileChanged(object sender, FileWatcherEventArgs e)
        {
            if (_filterManager.IsMatch(e?.AddedContent))
                System.Console.WriteLine("YES!!!");
        }
    }
}