using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IFormatReportRepository
    {
        Task<string> WriteReport(ICollection<ReportEntity> report);
    }
}
