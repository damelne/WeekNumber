using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Hardcodet.Wpf.TaskbarNotification;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WeekNumber.Services;

namespace WeekNumber.ViewModels
{
    public class TrayViewModel : ObservableObject
    {
        #region Private Fieds

        private readonly DispatcherTimer _timer;
        private readonly IAppSettingsService _settings;

        private System.Drawing.Icon _weekIcon;

        #endregion Private Fieds

        #region Constructor

        public TrayViewModel(IAppSettingsService settings)
        {
            _settings = settings;

            SelectedForegroundColor = (Color)ColorConverter.ConvertFromString(_settings.Settings.ForegroundColor);
            SelectedBackgroundColor = (Color)ColorConverter.ConvertFromString(_settings.Settings.BackgroundColor);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMinutes(10)
            };

            Refresh();

            _timer.Tick += (s, e) =>
            {
                Refresh();
            };

            _timer.Start();
        }

        #endregion Constructor

        #region Binding

        public int CurrentWeek => ISOWeek.GetWeekOfYear(DateTime.Now);

        public System.Drawing.Icon WeekIcon
        {
            get => _weekIcon;
            set
            {
                if (SetProperty(ref _weekIcon, value))
                    OnPropertyChanged(nameof(WeekIconImageSource));
            }
        }

        public ImageSource WeekIconImageSource => Imaging.CreateBitmapSourceFromHIcon(_weekIcon.Handle,
                                                                              Int32Rect.Empty,
                                                                              BitmapSizeOptions.FromEmptyOptions());

        public string TrayText =>
            $"KW {ISOWeek.GetWeekOfYear(DateTime.Now)}";

        private Color _selectedForegroundColor;

        public Color SelectedForegroundColor
        {
            get => _selectedForegroundColor;
            set
            {
                if (SetProperty(ref _selectedForegroundColor, value))
                {
                    _settings.Settings.ForegroundColor = _selectedForegroundColor.ToString();
                    _settings.Save();
                    Refresh();
                }
            }
        }

        private Color _selectedBackgroundColor;

        public Color SelectedBackgroundColor
        {
            get => _selectedBackgroundColor;
            set
            {
                if (SetProperty(ref _selectedBackgroundColor, value))
                {
                    _settings.Settings.BackgroundColor = _selectedBackgroundColor.ToString();
                    _settings.Save();
                    Refresh();
                }
            }
        }

        public ICommand OpenCommand => new RelayCommand(OpenSettings);
        public ICommand ExitCommand => new RelayCommand(Exit);

        public ICommand CloseCommand => new RelayCommand<Window>(CloseSettings);

        #endregion Binding

        #region Private Methods

        public void Refresh()
        {
            int week = ISOWeek.GetWeekOfYear(DateTime.Now);
            var trayIcon = (TaskbarIcon)Application.Current.Resources["TrayIcon"];
            trayIcon.Icon = TrayIconGenerator.CreateWeekIcon(week, new SolidColorBrush(SelectedForegroundColor), new SolidColorBrush(SelectedBackgroundColor));
            WeekIcon = trayIcon.Icon;
            OnPropertyChanged(nameof(TrayText));
        }

        #endregion Private Methods

        #region Commands

        private void OpenSettings()
        {
            // Prüfen, ob das Window schon offen ist
            var window = Application.Current.Windows
                          .OfType<MainWindow>()
                          .FirstOrDefault();

            if (window != null)
            {
                // Window schon offen → in den Vordergrund holen
                if (window.WindowState == WindowState.Minimized)
                    window.WindowState = WindowState.Normal;

                window.Activate(); // fokussiert das Fenster
            }
            else
            {
                // Window noch nicht offen → neu erstellen
                window = new MainWindow();
                window.Show();
            }
        }

        private void CloseSettings(Window? w)
        {
            if (w is Window window)
                window.Close();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        #endregion Commands
    }
}