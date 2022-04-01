using Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Entities;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Module_10.UnitTests")]
namespace BussinessLogic.Report
{
    class JsonReport : IFormatReport
    {
        public async Task<string> WriteReportAsync(ICollection<ReportEntity> report)
        {
            string json;
            using (var stream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(stream, report.ToList());
                stream.Position = 0;
                using var reader = new StreamReader(stream);
                json = await reader.ReadToEndAsync();
            }
            return json;
        }
    }
}
