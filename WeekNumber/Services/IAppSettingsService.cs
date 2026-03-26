using WeekNumber.Settings;

namespace WeekNumber.Services
{
    public interface IAppSettingsService
    {
        AppSettings Settings { get; }
        void Save();
    }
}