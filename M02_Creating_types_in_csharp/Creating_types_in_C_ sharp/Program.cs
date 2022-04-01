using System;
using System.Collections.Generic;
using System.Text;

namespace Creating_types_in_C__sharp
{
    class Program
    {
        static void Main(string[] args)
        {                  
            string[] subject = new string[] {"Maths", "PE", "Phisics", "English", "Chemistry", "Biology"};
            var student1c1 = new Student("Sanya.Posov@epam.com");
            var student2c1 = new Student("Erbolat.Kaldubaev@epam.com");
            var student3c1 = new Student("Arkadiy.Paravozov@epam.com");

            var student1c2 = new Student("Sanya", "Posov");
            var student2c2 = new Student("Erbolat", "Kaldubaev");
            var student3c2 = new Student("Arkadiy", "Paravozov");

            var studentSubjectDict = new Dictionary<Student, HashSet<string>>
            {
                [student1c1] = GetRandomSubjects(subject),
                [student2c1] = GetRandomSubjects(subject),
                [student3c1] = GetRandomSubjects(subject),
                [student1c2] = GetRandomSubjects(subject),
                [student2c2] = GetRandomSubjects(subject),
                [student3c2] = GetRandomSubjects(subject)
            };

            Console.WriteLine("There are three Student:\n");

            foreach (var item in studentSubjectDict)
            {
                Console.WriteLine($"{item.Key} , {ShowMassiv(item.Value)})");
            }           
        } 

        private static HashSet<string> GetRandomSubjects(string[] array)
        {
            var rnd = new Random();
            var set = new HashSet<string>();
                    
            while (set.Count<3)
            {
                set.Add(array[rnd.Next(array.Length)]);
            }
            return set;
        }

        private static string ShowMassiv(HashSet<string> hashset)
        {
            var sb = new StringBuilder();
            foreach (var item in hashset)
            {
                sb.Append(item).Append(' ');
            }
            return sb.ToString();
        }
    }
}
