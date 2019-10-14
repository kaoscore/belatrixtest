using LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LosHelper.IntegrationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowErrorOnConsole();
            ShowMessageOnConsole();
            ShowWarningOnConsole();
            SaveLogToFile();
            SaveLogToDataBase();
            Console.ReadLine();
        }

        private static void SaveLogToDataBase()
        {
            var log = new JobLogger(LogToTarget.Database, true, true, true);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void SaveLogToFile()
        {
            var log = new JobLogger(LogToTarget.File, true, true, true);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void ShowWarningOnConsole()
        {
            var log = new JobLogger(LogToTarget.Console, true, true, true);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void ShowMessageOnConsole()
        {
            var log = new JobLogger(LogToTarget.Console, true, true, true);

            log.LogMessage("a", MessageType.Message);
        }

        private static void ShowErrorOnConsole()
        {
            var log = new JobLogger(LogToTarget.Console, true, true, true);

            log.LogMessage("a", MessageType.Error);
        }
    }
}
