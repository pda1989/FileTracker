using Autofac;
using FileTracker.Common.Interfaces;
using System.Threading.Tasks;

namespace FileTracker.Common.Implementations
{
    public class Bootstraper
    {
        public static IContainer Container { get; set; }

        public Task InitAsync()
        {
            BuildContainer1();
            InitSettings();

            return Task.CompletedTask;
        }

        private void BuildContainer1()
        {
            var builder = new ContainerBuilder();

            // Register types
            builder.RegisterType<Settings>().AsSelf().As<ISettings>().SingleInstance();
            builder.RegisterType<JsonSettingsProvider>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FileIO>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FileWatcher>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ConsoleLogger>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ChangeTracker>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<FilterManager>().AsImplementedInterfaces().SingleInstance();

            Container = builder.Build();
        }

        private void InitSettings()
        {
            var settingsProvider = Container.Resolve<ISettingsProvider>();
            settingsProvider.InitSettings();
        }
    }
}