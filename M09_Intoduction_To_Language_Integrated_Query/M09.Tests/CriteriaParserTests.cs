using M09_Intoduction_To_Language_Integrated_Query;
using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace M09.Tests
{
    [TestFixture]
    public class CriteriaParserTests
    {
        static readonly Regex regex = new(@"(-[a-z]* [a-zA-Z\d\/ ]*)");

        [TestCase("-name Ivan -minmark 3 -sort maxmark asc 5 -datefrom 20/11/2012 -dateto 20/12/2016 -test Maths")]
        public void Parse_InputString_CriteriasClass(string inputTest)
        {
            var criterias = new Criterias();
            var criteriaParser = new CriteriaParser(criterias);
            var criteriasExpected = new Criterias()
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

            var criteriasActualed = criteriaParser.Parse(inputTest, regex);

            Assert.That(() => criteriasActualed.MaxMark, Is.EqualTo(criteriasExpected.MaxMark));
            Assert.That(() => criteriasActualed.MinMark, Is.EqualTo(criteriasExpected.MinMark));
            Assert.That(() => criteriasActualed.Name, Is.EqualTo(criteriasExpected.Name));
            Assert.That(() => criteriasActualed.SortName, Is.EqualTo(criteriasExpected.SortName));
            Assert.That(() => criteriasActualed.ToDoSort, Is.EqualTo(criteriasExpected.ToDoSort));
            Assert.That(() => criteriasActualed.Ascending, Is.EqualTo(criteriasExpected.Ascending));
            Assert.That(() => criteriasActualed.DateFrom, Is.EqualTo(criteriasExpected.DateFrom));
            Assert.That(() => criteriasActualed.DateTo, Is.EqualTo(criteriasExpected.DateTo));
            Assert.That(() => criteriasActualed.Test, Is.EqualTo(criteriasExpected.Test));
            Assert.That(() => criteriasActualed.Validator(niceGuy), Is.True);
            Assert.That(() => criteriasActualed.Validator(badGuy), Is.False);
        }
    }
}