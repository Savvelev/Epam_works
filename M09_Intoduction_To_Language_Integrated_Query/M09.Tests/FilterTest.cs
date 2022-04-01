using M09_Intoduction_To_Language_Integrated_Query;
using M09_Intoduction_To_Language_Integrated_Query.CriteriaContent;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M09.Tests
{
    [TestFixture]
    public class FilterTest
    {
        [Test]
        public void FilterByCritarias_CollectionAndCritarias_FiltredCollection()
        {
            var filter = new Filter();
            var students = new List<Student>
            {
                new Student
                {
                    Name = "Ivan",
                    Test = "Maths",
                    Date = DateTime.Parse("25/11/2012"),
                    Mark = 4
                },
                new Student
                {
                    Name = "Ivanov",
                    Test = "Maths",
                    Date = DateTime.Parse("25/11/2012"),
                    Mark = 3
                },
                new Student
                {
                    Name = "Ivanovich",
                    Test = "Maths",
                    Date = DateTime.Parse("20/11/2012"),
                    Mark = 5
                },
                new Student
                {
                    Name = "Kirill",
                    Test = "Maths",
                    Date = DateTime.Parse("25/11/2012"),
                    Mark = 4
                }
            }.AsReadOnly();

            var criterias = new Criterias
            {
                Name = "Ivan",
                MinMark = 3,
                MaxMark = 5,
                DateFrom = new DateTime(2012, 11, 20),
                DateTo = new DateTime(2016, 12, 20),
                Test = "Maths",
                ToDoSort = true,
                Ascending = true,
                SortName = "maxmark"
            };
            var validator = new StudentValidator(criterias);

            validator.AddFilterOption(StudentValidator.ValidationOptins.Name)
                     .AddFilterOption(StudentValidator.ValidationOptins.DateFrom)
                     .AddFilterOption(StudentValidator.ValidationOptins.DateTo)
                     .AddFilterOption(StudentValidator.ValidationOptins.MinMark)
                     .AddFilterOption(StudentValidator.ValidationOptins.MaxMark)
                     .AddFilterOption(StudentValidator.ValidationOptins.Test);

            criterias.Validator = validator.Validate;

            var expectedCollection = new List<Student>
            {
                new Student
                {
                    Name = "Ivanov",
                    Test = "Maths",
                    Date = DateTime.Parse("25/11/2012"),
                    Mark = 3
                },
                new Student
                {
                    Name = "Ivan",
                    Test = "Maths",
                    Date = DateTime.Parse("25/11/2012"),
                    Mark = 4
                },
                new Student
                {
                    Name = "Ivanovich",
                    Test = "Maths",
                    Date = DateTime.Parse("20/11/2012"),
                    Mark = 5
                }
            };

            var actualCollection = filter.FilterByCriterias(students, criterias);

            Assert.That(actualCollection.Count, Is.EqualTo(expectedCollection.Count));
            Assert.That(actualCollection.ElementAt(0).Name, Is.EqualTo(expectedCollection.ElementAt(0).Name));
            Assert.That(actualCollection.ElementAt(1).Test, Is.EqualTo(expectedCollection.ElementAt(1).Test));
            Assert.That(actualCollection.ElementAt(2).Date, Is.EqualTo(expectedCollection.ElementAt(2).Date));
            Assert.That(actualCollection.ElementAt(0).Mark, Is.EqualTo(expectedCollection.ElementAt(0).Mark));
        }
    }
}
