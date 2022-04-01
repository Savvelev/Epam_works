using System;
using System.IO;
using System.Text.RegularExpressions;


namespace M03_StringOverviewFormattingParsingComparing
{
    class PhoneNumberFinder
    {       
        static readonly Regex regex = new(@"([/+][\d]{0,3}\s|\d\s)(\d{3}|[/(]\d{0,3}[/)])\s\d{3}-\d{2}(-\d{2}|\d{2})");


        public static void FindPhoneNumbers()
        {          
            var fileInput = new FileInfo("Text.txt");
            var fileOutput = new FileInfo("Numbers.txt");
            var inputfromfile = ReadFromFile(fileInput);
            WriteToFile(inputfromfile, fileOutput);
        }

        private static void WriteToFile(string inputfromfile, FileInfo fileouput)
        {
            using (var sw = new StreamWriter(fileouput.FullName))
            {
                foreach (var item in regex.Matches(inputfromfile))
                {
                    sw.WriteLine(item.ToString());              
                }
            }
        }

        private static string ReadFromFile(FileInfo fileinput)
        {
            string inputfromfile;
            using (var sr = new StreamReader(fileinput.FullName))
            {
                inputfromfile = sr.ReadToEnd();
                if (string.IsNullOrEmpty(inputfromfile))
                {
                    throw new ArgumentException();
                }
            }

            return inputfromfile;
        }
    }
}
