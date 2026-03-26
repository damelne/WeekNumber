using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WeekNumber.Services;
using WeekNumber.ViewModels;

namespace WeekNumber
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Properties

        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// Der Applikationsname u.a. für Logging Source
        /// </summary>
        public static string? AppName => Application.Current.GetType().Assembly.GetName().Name;

        #endregion Properties

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceProvider = ConfigureServices();

            //Setzt den DataContext für die TaskbarIcon-Instanz, damit die Bindings in der XAML funktionieren
            var locator = (ViewModelLocator)Resources["ViewModelLocator"];
            var tray = (TaskbarIcon)Resources["TrayIcon"];
            tray.DataContext = locator.TrayViewModel;

            base.OnStartup(e);
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAppSettingsService, AppSettingsService>();
            services.AddTransient<TrayViewModel>();
            return services.BuildServiceProvider();
        }
    }
}