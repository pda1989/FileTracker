using Autofac;
using FileTracker.Common.Implementations;
using FileTracker.Common.Interfaces;
using FileTracker.Common.Models;

namespace FileTracker.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bootstraper = new Bootstraper();
            bootstraper.InitAsync().Wait();

            using (var watcher = Bootstraper.Container.Resolve<IFileWatcher>())
            {
                while (true)
                {
                    var line = System.Console.ReadLine();

                    if (line.Equals("exit", System.StringComparison.OrdinalIgnoreCase))
                        break;

                    if (line.Equals("start", System.StringComparison.OrdinalIgnoreCase))
                    {
                        var settings = Bootstraper.Container.Resolve<ISettings>();
                        watcher.WatchFiles(settings.FilePath, settings.FileMask);
                        watcher.OnFileChanged += OnFileChanged;
                    }

                    if (line.Equals("stop", System.StringComparison.OrdinalIgnoreCase))
                    {
                        watcher.StopWatching();
                    }
                }
            }
        }

        private static void OnFileChanged(object sender, FileWatcherEventArgs e)
        {
            if (e?.AddedContent?.Equals("123") ?? false)
                System.Console.WriteLine("YES!!!");
        }
    }
}