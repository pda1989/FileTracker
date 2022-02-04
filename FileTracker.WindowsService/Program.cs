using FileTracker.Common.Implementations;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace FileTracker.WindowsService
{
    internal static class Program
    {
        private static async Task InitAsync()
        {
            var bootstraper = new Bootstraper();
            await bootstraper.InitAsync();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static async void Main()
        {
            await InitAsync();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WindowsService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}