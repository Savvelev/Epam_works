using BussinessLogic.Report;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Module_10.UnitTests
{

    [TestFixture]
    public class JsonReportTests
    {
        [Test]
        public async Task WriteReportAsync_Report_JsonReport()
        {          
            // Arrange
            var jsonReport = new JsonReport();
            var testReport = new ReportSetUp().TestReport;
            
            // Action
            var actualJson = await jsonReport.WriteReportAsync(testReport);

            // Assert
            var expectedJson = JsonSerializer.Deserialize<List<ReportEntity>>(actualJson);

            for (int i = 0; i < testReport.Count; i++)
            {
                Assert.That(testReport[i].Lecture, Is.EqualTo(expectedJson[i].Lecture));
                Assert.That(testReport[i].StudentName, Is.EqualTo(expectedJson[i].StudentName));
                Assert.That(testReport[i].AttendingLecture, Is.EqualTo(expectedJson[i].AttendingLecture));
            }
        }
    }
}