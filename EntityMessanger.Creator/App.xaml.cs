using EntityMessanger.Creator.ViewModels;
using EntityMessanger.Domain.Services;
using EntityMessanger.EntityAPI.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows;

namespace EntityMessanger.Creator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider Services { get; }
        public new static App Current => (App)Application.Current;

        public App()
        {
            Services = ConfigureServices();
            ShowMainWindow();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            HubConnection connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7053/entitymessanger").Build();

            // Add Services
            services.AddSingleton<IEntityMessangerService>(serviceProvider => {
                var signalRMessangerService = new SignalRMessangerService(connection);
                Observable.FromAsync(signalRMessangerService.Connect)
                    .Retry(3)
                    .Timeout(TimeSpan.FromSeconds(10))
                    .Catch((Exception ex) => {
                        Console.WriteLine($"Error connecting to SignalR server: {ex.Message}");
                        MessageBox.Show("Unable to connect to entity messanger hub.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return Observable.Return(Unit.Default);
                    })
                    .Subscribe(_ => Console.WriteLine($"Connection succeeded to SignalR server"));
                return signalRMessangerService;
            });

            // Add Viewmodels
            services.AddTransient<MainViewModel>();
            services.AddTransient<EntityCreatorViewModel>();

            return services.BuildServiceProvider();
        }
        private void ShowMainWindow()
        {
            var mainViewModel = Services.GetService<MainViewModel>();
            var mainWindow = new MainWindow(mainViewModel);

            mainWindow.Show();
        }
    }
}
