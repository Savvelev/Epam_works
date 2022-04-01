using M09_Intoduction_To_Language_Integrated_Query;
using M09_Intoduction_To_Language_Integrated_Query.CriteriaContent;
using NUnit.Framework;
using System;

namespace M09.Tests
{
    [TestFixture]
    public class StudentValidatorTest
    {
        [Test]
        public void Validate_Student_Boolean()
        {
            var niceGuy = new Student
            {
                Name = "Ivan",
                Test = "Maths",
                Date = DateTime.Parse("25/11/2012"),
                Mark = 4
            };
            var badGuy = new Student
            {
                Name = "",
                Test = "",
                Date = default,
                Mark = default
            };
            var criterias = new Criterias()
            {
                Name = "Ivan",
                MinMark = 3,
                MaxMark = 5,
                DateFrom = new DateTime(2012, 11, 20),
                DateTo = new DateTime(2016, 12, 20),
                Test = "Maths",
                ToDoSort = true,
                Ascending = true,
                SortName = "maxmark",
            };
            var studentValidator = new StudentValidator(criterias);

            studentValidator.AddFilterOption(StudentValidator.ValidationOptins.Name)
                            .AddFilterOption(StudentValidator.ValidationOptins.DateFrom)
                            .AddFilterOption(StudentValidator.ValidationOptins.DateTo)
                            .AddFilterOption(StudentValidator.ValidationOptins.MinMark)
                            .AddFilterOption(StudentValidator.ValidationOptins.MaxMark)
                            .AddFilterOption(StudentValidator.ValidationOptins.Test);

            Assert.That(studentValidator.Validate(niceGuy), Is.True);
            Assert.That(studentValidator.Validate(badGuy), Is.False);
        }
    }
}
