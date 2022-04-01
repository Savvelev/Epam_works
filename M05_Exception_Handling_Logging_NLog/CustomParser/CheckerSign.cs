
namespace CustomParser
{
    public class CheckerSign
    {
        public static string CheckSign(string str, out bool isMinusSign)
        {          
            isMinusSign = str[0].Equals('-');

            if (isMinusSign || str[0].Equals('+'))
            {
                return str.Remove(0, 1);
            }

            return str;
        }
    }
}