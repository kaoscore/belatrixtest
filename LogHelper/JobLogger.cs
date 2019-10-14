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
        private static MessageType[] _onlyShow;
             
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="logToTarget">Target to Log</param>
        /// <param name="onlyShow">Message type selected to show</param>
        public JobLogger(LogToTarget logToTarget, MessageType[] onlyShow)
        {
            _onlyShow = onlyShow;
            _logToTarget = logToTarget;

        }
        /// <summary>
        /// Log Message Method
        /// </summary>
        /// <param name="message_to_log">Message to Log</param>
        /// <param name="messageType">Message Type</param>
        public void LogMessage(string message_to_log, MessageType messageType)
        {
           
            if (message_to_log == null )
            {
                throw new ArgumentNullException();
            }

            message_to_log.Trim();

            if (message_to_log.Length == 0)
            {
                throw new ArgumentNullException();
            }
           
            if (ShowLog(messageType))
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
            else
                throw new Exception("Invalid configuration");
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
            
            Console.WriteLine(string.Format("{0}:{1}", DateTime.Now.ToShortDateString(), message_to_log));
        }

        private void ToFile(string message_to_log, MessageType messageType)
        {
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
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
            catch
            {
                throw new Exception("Error when save log on file");
            }

           
        }

        private void ToDatabase(string message_to_log, MessageType messageType)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnectionString"]))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("Insert into Log Values('" + message_to_log + "', " + messageType.ToString() + ")");
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error when save log on database");
            }
           
        }

        /// <summary>
        /// Verify if Message Type is able to show
        /// </summary>
        /// <param name="messageType"></param>
        /// <returns></returns>
        private bool ShowLog(MessageType messageType)
        {
            bool canShow = false;

            if (_onlyShow == null)
                return false;

            foreach (MessageType item in _onlyShow)
            {
                if(item == messageType)
                {
                    canShow = true;
                }

            }
            return canShow;
                
        }

    }     
}
