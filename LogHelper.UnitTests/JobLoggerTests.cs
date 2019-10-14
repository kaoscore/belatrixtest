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
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger( LogToTarget.Console, onlyShow);

            Assert.That(() => log.LogMessage("", MessageType.Error), Throws.ArgumentNullException);                                  

        }
        [Test]
        public void LogMessage_MessageIsNull_ThrowArgNullException()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.Console, onlyShow);

            Assert.That(() => log.LogMessage(null, MessageType.Error), Throws.ArgumentNullException);

        }
       
     
        [TestCase(MessageType.Error, new MessageType[] { MessageType.Message })]
        [TestCase(MessageType.Message, new MessageType[] { MessageType.Error })]
        [TestCase(MessageType.Warning, new MessageType[] { MessageType.Error })]
        public void LogMessage_InvalidArguments_ThrowException(MessageType messageType, MessageType[] onlyShow)
        {
            var log = new JobLogger(LogToTarget.Console, onlyShow);

            Assert.That(() => log.LogMessage("a", messageType), Throws.Exception);

        }

        [Test]
        public void LogMessage_ToFileNoExists_CreateFile()
        {
            MessageType[] onlyShow = { MessageType.Error, MessageType.Message, MessageType.Warning };
            var log = new JobLogger(LogToTarget.File, onlyShow);

            FileAssert.Exists(@"D:\temp\LogFile" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt");
           
        }
    }
}
