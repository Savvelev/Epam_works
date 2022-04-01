using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IWarningService
    {
        Task<bool> MarkCourseWarning(string LastName, string firstName);
        Task<bool> AttendanceWarning(string LastName, string firstName);
    }
}
