using System.Windows;

namespace WeekNumber
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var vm = new TrayViewModel();

            foreach (var resource in Resources.Values)
            {
                if (resource is FrameworkElement element)
                    element.DataContext = vm;
            }
        }
    }
}