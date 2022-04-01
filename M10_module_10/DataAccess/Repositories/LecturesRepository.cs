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
    internal class LecturesRepository : ILectureRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<LecturesRepository> logger;

        public LecturesRepository(UniversityDbContext lecturesDbContext, IMapper mapper, ILogger<LecturesRepository> logger)
        {
            context = lecturesDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<bool> CreateAsync(Lecture item)
        {
            var lectureDb = mapper.Map<LectureDb>(item);
            await context.Lectures.AddAsync(lectureDb);
            return await ContextSaver<LecturesRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lectureToDelete = await context.Lectures.FindAsync(id);
            context.Entry(lectureToDelete).State = EntityState.Deleted;
            return await ContextSaver<LecturesRepository>.SaveAsync(context, logger);
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            var lectureDb = await context.Lectures
                .ToListAsync();
            return mapper.Map<ICollection<Lecture>>(lectureDb);
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {
            var lectureDb = await context.Lectures
                .FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<Lecture>(lectureDb);
        }

        public async Task<IEnumerable<Lecture>> GetLecturesIncludeStudentAsync()
        {
            var lectureDb = await context.Lectures
               .Include(l => l.Attendances)
               .ThenInclude(st => st.Student)
               .ToListAsync();
            return mapper.Map<ICollection<Lecture>>(lectureDb);
        }

        public async Task<bool> UpdateAsync(Lecture item, int id)
        {
            if (await context.Lectures.FindAsync(id) is LectureDb lectureInDb)
            {
                lectureInDb.Name = item.Name;
                context.Entry(lectureInDb).State = EntityState.Modified;
                return await ContextSaver<LecturesRepository>.SaveAsync(context, logger);
            }
            else
                throw new LectureNotFoundException(id);
        }
    }
}
