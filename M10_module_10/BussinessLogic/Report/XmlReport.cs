using Domain.Entities;
using Domain.Interfaces.Services;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Xml.Serialization;

[assembly: InternalsVisibleTo("Module_10.UnitTests")]
namespace BussinessLogic.Report
{
    class XmlReport : IFormatReport
    {
        public async Task<string> WriteReportAsync(ICollection<ReportEntity> report)
        {

            XmlSerializer xmlSerializer = new(typeof(List<ReportEntity>));

            using (StringWriter textWriter = new())
            {
                await Task.Run(() => xmlSerializer.Serialize(textWriter, report));
                return textWriter.ToString();
            }
        }
    }
}