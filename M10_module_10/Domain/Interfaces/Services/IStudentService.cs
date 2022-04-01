using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IStudentService : ICRUD<Student>
    {
        Task<string> Report(string StudentFirstName, string LastName, FormatSourse formatReport);
        Task<bool> AddStudentToAttendance(int studentId, int attendanceId);
    }
}
