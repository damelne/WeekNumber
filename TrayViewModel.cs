using CommunityToolkit.Mvvm.Input;
using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace WeekNumber
{
    public class TrayViewModel
    {
        private readonly DispatcherTimer _timer;
        private NotifyIcon _trayIcon = new NotifyIcon();

        public int CurrentWeek => ISOWeek.GetWeekOfYear(DateTime.Now);

        public event PropertyChangedEventHandler PropertyChanged;

        public string TrayText =>
            $"KW {ISOWeek.GetWeekOfYear(DateTime.Now)}";

        public ICommand OpenCommand => new RelayCommand(Open);
        public ICommand ExitCommand => new RelayCommand(Exit);

        public TrayViewModel()
        {
           

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

        public void Refresh()
        {
            int week = ISOWeek.GetWeekOfYear(DateTime.Now);
            var trayIcon = (TaskbarIcon)System.Windows.Application.Current.Resources["TrayIcon"];
            trayIcon.Icon = TrayIconGenerator.CreateWeekIcon(week);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TrayText)));
        }

        private void Open()
        {
            var window = new MainWindow();
            window.Show();
        }

        private void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }


    }
}
