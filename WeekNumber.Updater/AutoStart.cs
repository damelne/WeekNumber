using Microsoft.Win32;

namespace WeekNumber.Updater;

internal static class AutoStart
{
    private const string RunKey =
        @"Software\Microsoft\Windows\CurrentVersion\Run";

    /// <summary>
    /// Setzt einen Autostart-Eintrag für den aktuellen Benutzer.
    /// </summary>
    internal static void Set(string appName, string appPath)
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true)
                ?? throw new InvalidOperationException($"Registry-Key nicht gefunden: {RunKey}");

            key.SetValue(appName, $"\"{appPath}\"");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Autostart konnte nicht gesetzt werden: {ex.Message}");
        }
    }

    /// <summary>
    /// Entfernt den Autostart-Eintrag (für Uninstall).
    /// </summary>
    internal static void Remove(string appName)
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(RunKey, writable: true);
            key?.DeleteValue(appName, throwOnMissingValue: false);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Autostart konnte nicht entfernt werden: {ex.Message}");
        }
    }
}