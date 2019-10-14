using System;
using System.Data.SqlClient;
using System.IO;

namespace LogHelper
{
    public enum LogToTarget
    {
        File, Console, Database
    }

    public enum MessageType
    {
        Message, Warning, Error
    }
    public interface IJobLogger
    {
        void LogMessage(string message_to_log, MessageType messageType);
    }
    public class JobLogger : IJobLogger
    {
        private static LogToTarget _logToTarget;

        private static bool _logMessage;
        private static bool _logWarning;
        private static bool _logError;

    
        public JobLogger(LogToTarget logToTarget, bool logMessage, bool logWarning, bool logError)
        {
            _logError = logError;
            _logMessage = logMessage;
            _logWarning = logWarning;
            _logToTarget = logToTarget;

        }
        public void LogMessage(string message_to_log, MessageType messageType)
        {
            message_to_log.Trim();
            if (message_to_log == null || message_to_log.Length == 0)
            {
                return;
            }

            if (!_logError && !_logMessage && !_logWarning)
            {
                throw new Exception("Error or Warning or Message must be specified");
            }

            if (
                   (messageType == MessageType.Message && _logMessage)
                || (messageType == MessageType.Error && _logError)
                || (messageType == MessageType.Warning && _logWarning)
                )
            {
                switch (_logToTarget)
                {
                    case LogToTarget.Database:
                        ToDatabase(message_to_log, messageType);
                        break;
                    case LogToTarget.File:
                        ToFile(message_to_log, messageType);
                        break;
                    case LogToTarget.Console:
                        ToConsole(message_to_log, messageType);
                        break;
                    default:
                        throw new Exception("Invalid configuration");
                 }
            }
        }

        private void ToConsole(string message_to_log, MessageType messageType)
        {
            if (messageType == MessageType.Error )
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            if (messageType == MessageType.Warning)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            if (messageType == MessageType.Message)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(DateTime.Now.ToShortDateString() + message_to_log);
        }

        private void ToFile(string message_to_log, MessageType messageType)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToShortDateString(), message_to_log));
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToShortDateString(), message_to_log));
                }
            }
        }

        private void ToDatabase(string message_to_log, MessageType messageType)
        {
            using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("Insert into Log Values('" + message_to_log + "', " + messageType.ToString() + ")");
                command.ExecuteNonQuery();
            }
        }
    }     
}
