using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IStudentRepository : ICRUD<Student>
    {
        Task<IEnumerable<Student>> GetStudentsIncludeLectureAsync();
        Task<bool> AddStudentToAttendance(int studentId, int attendanceId);
    }
}
