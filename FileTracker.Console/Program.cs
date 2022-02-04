using Autofac;
using FileTracker.Common.Implementations;
using FileTracker.Common.Interfaces;

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
                        watcher.WatchFile(settings.FilePath, settings.FileMask);
                    }

                    if (line.Equals("stop", System.StringComparison.OrdinalIgnoreCase))
                    {
                        watcher.StopWatching();
                    }
                }
            }
        }
    }
}