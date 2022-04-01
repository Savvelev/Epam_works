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
    class HomeworksRepository : IHomeworkRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<HomeworksRepository> logger;

        public HomeworksRepository(UniversityDbContext homeworkDbContext, IMapper mapper, ILogger<HomeworksRepository> logger)
        {
            context = homeworkDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<bool> CreateAsync(Homework item)
        {
            var homeworkDb = mapper.Map<HomeworkDb>(item);
            await context.Homeworks.AddAsync(homeworkDb);
            return await ContextSaver<HomeworksRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var homeworkToDelete = await context.Homeworks.FindAsync(id);
            context.Entry(homeworkToDelete).State = EntityState.Deleted;
            return await ContextSaver<HomeworksRepository>.SaveAsync(context, logger);

        }

        public async Task<IEnumerable<Homework>> GetAllAsync()
        {
            var homeworkDb = await context.Homeworks
                .ToListAsync();
            return mapper.Map<ICollection<Homework>>(homeworkDb);
        }

        public async Task<Homework> GetByIdAsync(int id)
        {
            var homeworkDb = await context.Homeworks
                .FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<Homework>(homeworkDb);
        }

        public async Task<bool> UpdateAsync(Homework item, int id)
        {
            if (await context.Homeworks.FindAsync(id) is HomeworkDb homeworkInDb)
            {
                homeworkInDb.Name = item.Name;
                homeworkInDb.CourseMark = item.CourseMark;
                context.Entry(homeworkInDb).State = EntityState.Modified;
                return await ContextSaver<HomeworksRepository>.SaveAsync(context, logger);
            }
            else
                throw new HomeworkNotFoundException(id);
        }
    }
}
