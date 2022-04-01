using System;
using System.Collections.Generic;
using System.Text;

namespace M03_StringOverviewFormattingParsingComparing
{
    class StringReverser
    {
        public static string ReverseString(string input)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))         
                throw new ArgumentException();
            
            var splitedstring = SplitStringToMassivOfSubstring.SplitingWithNull(input);          
            var reversingmassiv = new Stack<string>();
            var sb = new StringBuilder();

            foreach (var item in splitedstring)
            {
                reversingmassiv.Push(item);
            }

            while (reversingmassiv.Count != 0)
            {
                sb.Append(reversingmassiv.Pop());
            }

            return sb.ToString();
        }
    }
}
