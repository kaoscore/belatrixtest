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
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.Database, onlyShow);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void SaveLogToFile()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.File, onlyShow);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void ShowWarningOnConsole()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.Console, onlyShow);

            log.LogMessage("a", MessageType.Warning);
        }

        private static void ShowMessageOnConsole()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.Console, onlyShow);

            log.LogMessage("a", MessageType.Message);
        }

        private static void ShowErrorOnConsole()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.Console, onlyShow);

            log.LogMessage("a", MessageType.Error);
        }
    }
}
