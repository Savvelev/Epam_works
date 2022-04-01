using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IFormatReport
    {
        Task<string> WriteReportAsync(ICollection<ReportEntity> report);
    }
}