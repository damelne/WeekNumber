namespace WeekNumber.Updater
{
    internal static class Commands
    {
        /// <summary>
        /// --stop &lt;ProcessName&gt;
        /// Beendet alle Instanzen des angegebenen Prozesses.
        /// </summary>
        internal static int Stop(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("--stop: Prozessname fehlt.");
                return ExitCode.MissingArgument;
            }

            var processName = Path.GetFileNameWithoutExtension(args[1]);
            ProcessHelper.Kill(processName);
            return ExitCode.Success;
        }

        /// <summary>
        /// --post-install &lt;AppPath&gt;
        /// Setzt Autostart-Verknüpfung und Tray-Icon-Sichtbarkeit.
        /// </summary>
        internal static int PostInstall(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("--post-install: AppPath fehlt.");
                return ExitCode.MissingArgument;
            }

            var appPath = args[1];
            var appName = Path.GetFileNameWithoutExtension(appPath);

            AutoStart.Set(appName, appPath);

            return ExitCode.Success;
        }

        /// <summary>
        /// --uninstall &lt;AppName&gt;
        /// Entfernt Autostart-Eintrag.
        /// </summary>
        internal static int Uninstall(string[] args)
        {
            if (args.Length < 2)
            {
                Console.Error.WriteLine("--uninstall: AppName fehlt.");
                return ExitCode.MissingArgument;
            }

            AutoStart.Remove(args[1]);
            return ExitCode.Success;
        }
    }
}