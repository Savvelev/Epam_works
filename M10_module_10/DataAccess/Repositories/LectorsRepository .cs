using AutoMapper;
using DataAccess.DbEntities;
using Domain.Entities;
using Domain.Exceptions.NotFound;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class LectorsRepository : ILectorRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<LectorsRepository> logger;

        public LectorsRepository(UniversityDbContext lectorsDbContext, IMapper mapper, ILogger<LectorsRepository> logger)
        {
            context = lectorsDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }
        public async Task<bool> CreateAsync(Lector item)
        {
            var lectorDb = mapper.Map<LectorDb>(item);
            await context.Lectors.AddAsync(lectorDb);
            return await ContextSaver<LectorsRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lectorToDelete = await context.Lectors.FindAsync(id);
            context.Entry(lectorToDelete).State = EntityState.Deleted;
            return await ContextSaver<LectorsRepository>.SaveAsync(context, logger);
        }

        public async Task<IEnumerable<Lector>> GetAllAsync()
        {
            var lectorDb = await context.Lectors
                .ToListAsync();
            return mapper.Map<ICollection<Lector>>(lectorDb);
        }

        public async Task<Lector> GetByIdAsync(int id)
        {
            var lectorDb = await context.Lectors
                .FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<Lector>(lectorDb);
        }

        public async Task<bool> UpdateAsync(Lector item, int id)
        {
            if (await context.Lectors.FindAsync(id) is LectorDb lectorInDb)
            {
                lectorInDb.FirstName = item.FirstName;
                lectorInDb.LastName = item.LastName;
                lectorInDb.Email = item.Email;
                context.Entry(lectorInDb).State = EntityState.Modified;
                return await ContextSaver<LectorsRepository>.SaveAsync(context, logger);
            }
            else
                throw new LectorNotFoundException(id);
        }
        public async Task<ICollection<Lector>> GetLectorFromAttendance()
        {
            var lectorsdb = await context.Lectors
                 .Include(x => x.Lectures)
                 .ThenInclude(x => x.Attendances)
                 .ToListAsync();
            return mapper.Map<ICollection<Lector>>(lectorsdb);

        }
    }
}
