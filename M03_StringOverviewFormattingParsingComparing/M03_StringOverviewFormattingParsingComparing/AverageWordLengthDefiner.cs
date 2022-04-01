using System;

namespace M03_StringOverviewFormattingParsingComparing
{
    internal class AverageWordLengthDefiner
    {                    
        public static double DefineAverageWordLength(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
            {
                throw new ArgumentException(nameof(input));
            }
            
            var splitedstring = SplitStringToMassivOfSubstring.Spliting(input);
            var numberofString = (double)splitedstring.Length;
            var sumofNumbers = 0;

            foreach (var item in splitedstring)
            {
                sumofNumbers += item.Length;
            }

            return sumofNumbers / numberofString;
        }
    }
}
