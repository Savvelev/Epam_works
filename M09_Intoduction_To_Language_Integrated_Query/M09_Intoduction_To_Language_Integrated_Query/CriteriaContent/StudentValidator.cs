using System;
using System.Collections.Generic;
using System.Linq;

namespace M09_Intoduction_To_Language_Integrated_Query.CriteriaContent
{
    public class StudentValidator
    {

        private readonly Criterias criterias;
        private readonly List<Func<Student, bool>> validators = new();

        public StudentValidator(Criterias criterias)
        {
            this.criterias = criterias;
        }

        private bool DateFrom(Student student) => student.Date >= criterias.DateFrom;
        private bool DateTo(Student student) => student.Date <= criterias.DateTo;
        private bool MinMark(Student student) => student.Mark >= criterias.MinMark;
        private bool MaxMark(Student student) => student.Mark <= criterias.MaxMark;
        private bool Name(Student student) => student.Name.Contains(criterias.Name);
        private bool Test(Student student) => student.Test == criterias.Test;

        public StudentValidator AddFilterOption(ValidationOptins opt)
        {
            switch (opt)
            {
                case ValidationOptins.DateFrom:
                    validators.Add(DateFrom);
                    break;
                case ValidationOptins.DateTo:
                    validators.Add(DateTo);
                    break;
                case ValidationOptins.MinMark:
                    validators.Add(MinMark);
                    break;
                case ValidationOptins.MaxMark:
                    validators.Add(MaxMark);
                    break;
                case ValidationOptins.Name:
                    validators.Add(Name);
                    break;
                case ValidationOptins.Test:
                    validators.Add(Test);
                    break;
                default:
                    break;
            }
            return this;
        }

        public bool Validate(Student student) => validators.All(v => v(student));

        public enum ValidationOptins
        {
            DateFrom,
            DateTo,
            MinMark,
            MaxMark,
            Name,
            Test
        }
    }
}
