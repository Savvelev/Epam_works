using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface ILectorRepository : ICRUD<Lector>
    {
        Task<ICollection<Lector>> GetLectorFromAttendance();
    }
}
