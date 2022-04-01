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
    class AttendanceRepository : IAttendanceRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<AttendanceRepository> logger;

        public AttendanceRepository(UniversityDbContext attendanceDbContext, IMapper mapper, ILogger<AttendanceRepository> logger)
        {
            context = attendanceDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<bool> CreateAsync(Attendance item)
        {
            var attendanceDb = mapper.Map<AttendanceDb>(item);
            await context.Attendances.AddAsync(attendanceDb);
            return await ContextSaver<AttendanceRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendanceToDelete = await context.Attendances.FindAsync(id);
            context.Entry(attendanceToDelete).State = EntityState.Deleted;
            return await ContextSaver<AttendanceRepository>.SaveAsync(context, logger);
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            var attendanceDb = await context.Attendances
                .ToListAsync();
            return mapper.Map<ICollection<Attendance>>(attendanceDb);
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            var attendanceDb = await context.Attendances
                .Include(x => x.Lecture)
                .Include(x => x.Student)
                .Include(x => x.Homework)
                .FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<Attendance>(attendanceDb);
        }

        public async Task<bool> UpdateAsync(Attendance item, int id)
        {
            if (await context.Attendances.FindAsync(id) is AttendanceDb attendanceInDb)
            {
                attendanceInDb.IsStudentAttended = item.IsStudentAttended;
                context.Entry(attendanceInDb).State = EntityState.Modified;
                return await ContextSaver<AttendanceRepository>.SaveAsync(context, logger);
            }
            else
                throw new AttendanceNotFoundException(id);
        }
    }
}