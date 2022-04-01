using System.Linq;
using Microsoft.Extensions.Logging;

namespace CustomParser
{
    public class StringParser
    {
        private readonly ILogger logger;

        public StringParser(ILogger<StringParser> logger)
        {
            this.logger = logger;
        }
       
        public int ParseFromStringToInt(string str)
        {
            ExceptionsForLog.LogAndThrowNullReferenceException(str, logger);

            var unsighedStr = CheckerSign.CheckSign(str.Trim(), out bool flag);
            var length = unsighedStr.Length;
            var currentindex = 0;
            var nextChar = unsighedStr[0];
            var result = 0;

            if (unsighedStr.All(n => char.IsDigit(n)))
            {             

                while(currentindex< length)
                {
                    nextChar = unsighedStr[currentindex++];
                  
                    result = result * 10 + (nextChar - '0');
                    if (result < 0) ExceptionsForLog.LogAndThrowOverflowException(logger);

                }                                                        
            }
            else
            {
                ExceptionsForLog.LogAndThrowArgumentException(logger);
            }
            return flag ? result * -1 : result;           
        }
    }
}
