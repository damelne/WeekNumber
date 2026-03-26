using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekNumber.Updater
{
    internal static class ExitCode
    {
        internal const int Success = 0;
        internal const int MissingArgument = 1;
        internal const int UnknownArgument = 2;
        internal const int GeneralError = -1;
    }
}
