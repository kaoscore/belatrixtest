using System;
using System.Text;

using NUnit.Framework;

namespace LogHelper.UnitTests
{
    
    [TestFixture]
    public class JobLoggerTests
    {
      
        [Test]
        public void LogMessage_MessageIsEmpty_ThrowArgNullException()
        {
            var log = new JobLogger( LogToTarget.Console,true,true, true);

            Assert.That(() => log.LogMessage("", MessageType.Error), Throws.ArgumentNullException);                                  

        }
        [Test]
        public void LogMessage_MessageIsNull_ThrowArgNullException()
        {
            var log = new JobLogger(LogToTarget.Console, true, true, true);

            Assert.That(() => log.LogMessage(null, MessageType.Error), Throws.ArgumentNullException);

        }
        [Test]
        public void LogMessage_AllChooseTypesAreFalse_ThrowException()
        {
            var log = new JobLogger(LogToTarget.Console, false, false, false);

            Assert.That(() => log.LogMessage("a", MessageType.Error), Throws.Exception);

        }

     
        [TestCase(MessageType.Error, true, true, false)]
        [TestCase(MessageType.Message, false, true, false)]
        [TestCase(MessageType.Warning, true, false, false)]
        public void LogMessage_InvalidArguments_ThrowException(MessageType messageType, bool logMessage, bool logWarning, bool logError)
        {
            var log = new JobLogger(LogToTarget.Console, logMessage, logWarning, logError);

            Assert.That(() => log.LogMessage("a", messageType), Throws.Exception);

        }

        [Test]
        public void LogMessage_ToFileNoExists_CreateFile()
        {
            var log = new JobLogger(LogToTarget.File, true, true, true);

            FileAssert.Exists(@"D:\temp\LogFile" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt");
           
        }
    }
}
