using BussinessLogic.Report;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Module_10.UnitTests
{
    [TestFixture]
    class XmlReportTests
    {
        [Test]
        public async Task WriteReportAsync_Report_XmlReport()
        {
            var xmlReport = new XmlReport();
            var testReport = new ReportSetUp().TestReport;

            var actualXml = await xmlReport.WriteReportAsync(testReport);
            List<ReportEntity> expectedXml;

            var serializer = new XmlSerializer(typeof(List<ReportEntity>));

            using (var sr = new StringReader(actualXml))
            {
                expectedXml = (List<ReportEntity>)serializer.Deserialize(sr);
            }

            for (int i = 0; i < testReport.Count; i++)
            {
                Assert.That(testReport[i].Lecture, Is.EqualTo(expectedXml[i].Lecture));
                Assert.That(testReport[i].StudentName, Is.EqualTo(expectedXml[i].StudentName));
                Assert.That(testReport[i].AttendingLecture, Is.EqualTo(expectedXml[i].AttendingLecture));
            }
        }
    }
}
