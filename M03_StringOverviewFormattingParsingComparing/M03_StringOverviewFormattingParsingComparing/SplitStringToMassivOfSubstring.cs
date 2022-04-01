using System;
using System.Text.RegularExpressions;

namespace M03_StringOverviewFormattingParsingComparing
{
    public class SplitStringToMassivOfSubstring
    {
        readonly static char[] mas = new char[] { ' ', '.', ',', '!', '?', ';', ':', '-' };
        readonly static Regex regex = new Regex("(?<=\\s)|(?=\\s)");
        public static string[] Spliting(string input) => input.Split(mas, StringSplitOptions.RemoveEmptyEntries);      
        public static string[] SplitingWithNull(string input) => regex.Split(input); 
        
    }
}
