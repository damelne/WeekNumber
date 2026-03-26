using System.Diagnostics;

namespace WeekNumber.Updater
{
    internal static class ProcessHelper
    {
        /// <summary>
        /// Beendet alle Prozesse mit dem angegebenen Namen.
        /// Versucht zuerst einen sauberen Shutdown, danach Kill.
        /// </summary>
        internal static void Kill(string processName, int gracefulTimeoutMs = 3000)
        {
            var processes = Process.GetProcessesByName(processName);

            foreach (var p in processes)
            {
                try
                {
                    p.CloseMainWindow();

                    if (!p.WaitForExit(gracefulTimeoutMs))
                        p.Kill();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Fehler beim Beenden von '{processName}': {ex.Message}");
                }
                finally
                {
                    p.Dispose();
                }
            }
        }
    }
}