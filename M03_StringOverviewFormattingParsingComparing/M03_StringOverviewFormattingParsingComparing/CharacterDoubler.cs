using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace M03_StringOverviewFormattingParsingComparing
{
    internal class CharacterDoubler
    {
        public static string DoubleCharacters(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                throw new ArgumentException();
            }

            var sb = new StringBuilder();
            var uniquechars = new HashSet<char>(str2);  
     
            foreach (var item in str1)
            {              
                sb.Append(item);
                            
                if (uniquechars.Contains(item) && item != ' ')        
                {                   
                    sb.Append(item);
                }
            }            
          
            return sb.ToString();
        }

    }
}
