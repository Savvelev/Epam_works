using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ILectureService : ICRUD<Lecture>
    {
        Task<string> Report(string Name, FormatSourse formatReport);
    }
}
