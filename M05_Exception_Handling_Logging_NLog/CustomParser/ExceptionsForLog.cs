using Microsoft.Extensions.Logging;
using System;

namespace CustomParser
{
    public class ExceptionsForLog
    {
        public static void LogAndThrowArgumentException(ILogger logger)
        {
            var ex = new ArgumentException();
            logger.Log(LogLevel.Error, ex.Message, ex);
            throw ex;
        }

        public static void LogAndThrowOverflowException(ILogger logger)
        {          
                var ex = new OverflowException("The value in the line is greater than the allowed one");
                logger.Log(LogLevel.Error, ex.Message, ex);
                throw ex;          
        }

        public static void LogAndThrowNullReferenceException(string str, ILogger logger)
        {
            if (string.IsNullOrEmpty(str))
            {
                var ex = new ArgumentNullException("Empty or Null string");
                logger.Log(LogLevel.Error, ex.Message, ex);
                throw ex;
            }
        }
    }
}