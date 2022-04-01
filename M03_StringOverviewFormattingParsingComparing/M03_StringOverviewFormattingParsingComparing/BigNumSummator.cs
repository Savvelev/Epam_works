using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M03_StringOverviewFormattingParsingComparing
{
    class BigNumSummator
   {
        public static string Sum(string num1, string num2)
        {
            IsValidInput(num1, num2);

            var digits = new List<byte>();
            var listByteNum1 = FromStringToListByte(num1);
            var listByteNum2 = FromStringToListByte(num2);                  
            var maxLength = Math.Max(num1.Length, num2.Length);
            byte upperTen = 0;

            for (int i = 0; i < maxLength; i++)
            {
                var n1 = (byte)(i < listByteNum1.Count ? byte.Parse(listByteNum1[i].ToString()) : 0);
                var n2 = (byte)(i < listByteNum2.Count ? byte.Parse(listByteNum2[i].ToString()) : 0);
                var sum = (byte)(n1 + n2 + upperTen);
               
                if (sum >= 10)
                {
                    sum -= 10;
                    upperTen = 1;
                }
                else
                {
                    upperTen = 0;
                }

                digits.Add(sum);
            }
            if (upperTen > 0)
            {
                digits.Add(upperTen);
            }

            digits.Reverse();
            return FromListByteToString(digits);
        }

        private static List<byte> FromStringToListByte(string inputString)
        {
            var list = new List<byte>();
            foreach (var c in inputString.Reverse())
            {
                list.Add(byte.Parse(c.ToString()));
            }

            return list;
        }
        private static string FromListByteToString(List<byte> inputList)
        {
            var sb = new StringBuilder();
            foreach (var item in inputList)
            {
                sb.Append(item);
            }

            return sb.ToString();         
        }

        private static void IsValidInput(string num1, string num2)
        {
            IsNotNull(num1, num2);
            IsDigit(num1, num2);
        }

        private static void IsDigit(string num1, string num2)
        {
            if (num1.Any(c => !char.IsDigit(c)) || num2.Any(c => !char.IsDigit(c)))
            {
                var argumentException = new ArgumentException();
                throw argumentException;
            }
        }

        private static void IsNotNull(string num1, string num2)
        {
            if (num1 == null || num2 == null)
            {
                var argumentNullException = new ArgumentNullException();
                throw argumentNullException;
            }
        }
    }
}
