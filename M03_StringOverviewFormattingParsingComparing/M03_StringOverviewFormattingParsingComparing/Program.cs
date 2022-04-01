using System;

namespace M03_StringOverviewFormattingParsingComparing
{
    class Program
    {       
        static void Main(string[] args)
        {

            Console.WriteLine(AverageWordLengthDefiner.DefineAverageWordLength(" 12"));
            Console.WriteLine(CharacterDoubler.DoubleCharacters("omg i love shrek", "o kek"));
            PhoneNumberFinder.FindPhoneNumbers();          
            Console.WriteLine( StringReverser.ReverseString("The greatest victory is that which requires no battle")); 

            Console.WriteLine(BigNumSummator.Sum("196900000", "2000000"));

            

        }              
    }
}
