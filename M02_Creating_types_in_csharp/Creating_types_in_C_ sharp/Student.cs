using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Creating_types_in_C__sharp
{
    class Student
    {
        public string FullName { get; init; }
        public string Email { get; init; }

        public Student(string email)
        {
            Email = email;
            FullName = GetFullNameFromEmail(email);
        }

        public Student(string name, string surname)
        {
            FullName = $"{name} {surname}";
            Email = GetEmailFromNameSurname(name, surname);
        }

        private static string GetEmailFromNameSurname(string name, string surname)
        {
            var sb = new StringBuilder();
            string sb1;

            sb.Append(name)
              .Append('.')
              .Append(surname)
              .Append("@epam.com");          

            return sb.ToString();
        }

        private static string GetFullNameFromEmail(string email)
        {
            var sb = new StringBuilder();
            var nameAndSurname = email.Split('.', '@');

            sb.Append(nameAndSurname[0])
              .Append(' ')
              .Append(nameAndSurname[1]);

            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Name: {FullName}, Email:{Email}" ;
        }

        public override bool Equals(object obj)
        {
            return (obj is Student s && s.FullName == FullName && s.Email == Email);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FullName, Email);
        }

    }
}
