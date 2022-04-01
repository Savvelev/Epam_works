using System;
using System.Text.RegularExpressions;

namespace M09_Intoduction_To_Language_Integrated_Query
{
    class Program 
    {
        static readonly Regex regex = new(@"(-[a-z]* [a-zA-Z\d\/ ]*)");

        static void Main(string[] args)
        {
            var consoleoutput = new ConsoleOutput();
            var jsonConverter = new Converter<Student>(new JsonConverter<Student>());
            var criterias = new Criterias();
            var student = jsonConverter.Converting();

            consoleoutput.Display();
            criterias = new CriteriaParser(criterias).Parse(Console.ReadLine(), regex);
            Console.Clear();

            var studentQuery = new Filter().FilterByCriterias(student, criterias);
            consoleoutput.OutputStudentCollection(studentQuery);        
        }       
    }
}

