using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ILectureRepository : ICRUD<Lecture>
    {
        Task<IEnumerable<Lecture>> GetLecturesIncludeStudentAsync();
    }
}
