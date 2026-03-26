using WeekNumber.Updater;

if (args.Length == 0)
{
    Console.Error.WriteLine("Kein Argument angegeben.");
    return ExitCode.MissingArgument;
}

return args[0] switch
{
    "--stop" => Commands.Stop(args),
    "--post-install" => Commands.PostInstall(args),
    "--uninstall" => Commands.Uninstall(args),
    _ => ExitCode.UnknownArgument
};