using System;
using System.Collections.Generic;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    public class ConsoleOutput
    {
        public void Display()
        {
            Console.WriteLine(" Hello! Please input your criteria: \n" +
                "Avalible criteria:\n-name\n-minmark\n-maxmark\n-datefrom-dateto\n-test\n-sort (one of criteria) asc\\desc\n" +
                "\nExample:\n-name Ivan -minmark 3 -sort maxmark asc 5 -datefrom 20/11/2012 -dateto 20/12/2012 -test Maths\n\nInput format:");
        }

        public void OutputStudentCollection(IEnumerable<Student> collection)
        {
            Console.WriteLine($"Student\tTest\tDate\tMark");
            foreach (var item in collection)
            {
                Write(item.ToFormatedString());
            }
        }
        private void Write(string input)
        {
            Console.WriteLine(input);
            Console.WriteLine();
        }
    }
}
