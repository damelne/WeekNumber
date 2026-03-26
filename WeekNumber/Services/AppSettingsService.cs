using System.IO;
using System.Text.Json;
using WeekNumber.Settings;

namespace WeekNumber.Services
{
    public class AppSettingsService : IAppSettingsService
    {
        private readonly string _filePath;

        public AppSettings Settings { get; private set; }

        public AppSettingsService()
        {
            _filePath = Path.Combine(
             Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
             "WeekNumber",
             "settings.json");

            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    Settings = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
                }
                else
                {
                    Settings = new AppSettings();
                }
            }
            catch (Exception ex)
            {
                // Falls die Datei beschädigt ist oder nicht gelesen werden kann
                Settings = new AppSettings();
                Console.WriteLine($"Fehler beim Laden der Settings: {ex.Message}");
            }
        }


        public void Save()
        {
            try
            {
                var dir = Path.GetDirectoryName(_filePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                // Fehler beim Schreiben
                Console.WriteLine($"Fehler beim Speichern der Settings: {ex.Message}");
            }
        }
    }
}