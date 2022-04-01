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
    internal class StudentsRepository : IStudentRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;
        private readonly ILogger<StudentsRepository> logger;

        public StudentsRepository(UniversityDbContext studentDbContext, IMapper mapper, ILogger<StudentsRepository> logger)
        {
            context = studentDbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<bool> AddStudentToAttendance(int studentId, int attendanceId)
        {
            var student = await context.Students.FirstOrDefaultAsync(st => st.Id == studentId);
            var attendance = await context.Attendances.FirstOrDefaultAsync(a => a.Id == attendanceId);
            student.Attendances.Add(attendance);
            return await ContextSaver<StudentsRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> CreateAsync(Student item)
        {
            var studentDb = mapper.Map<StudentDb>(item);
            await context.Students.AddAsync(studentDb);
            return await ContextSaver<StudentsRepository>.SaveAsync(context, logger);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var studentToDelete = await context.Students.FindAsync(id);
            context.Entry(studentToDelete).State = EntityState.Deleted;
            return await ContextSaver<StudentsRepository>.SaveAsync(context, logger);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var studentDb = await context.Students
                .ToListAsync();
            return mapper.Map<ICollection<Student>>(studentDb);
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var studentDb = await context.Students
                .Include(at => at.Attendances)
                .FirstOrDefaultAsync(p => p.Id == id);

            return mapper.Map<Student>(studentDb);
        }

        public async Task<IEnumerable<Student>> GetStudentsIncludeLectureAndLectorAsync()
        {
            var studentDb = await context.Students
              .Include(l => l.Attendances)
              .ThenInclude(z => z.Lecture)
              .ThenInclude(z => z.Lector)
              .ToListAsync();
            return mapper.Map<ICollection<Student>>(studentDb);
        }

        public async Task<IEnumerable<Student>> GetStudentsIncludeLectureAsync()
        {
            var studentDb = await context.Students
                .Include(l => l.Attendances)
                .ThenInclude(l => l.Lecture)
                .Include(x => x.Attendances)
                .ThenInclude(h => h.Homework)
                .ToListAsync();
            return mapper.Map<ICollection<Student>>(studentDb);
        }

        public async Task<bool> UpdateAsync(Student item, int id)
        {
            if (await context.Students.FindAsync(id) is StudentDb studentInDb)
            {
                studentInDb.FirstName = item.FirstName;
                studentInDb.LastName = item.LastName;
                studentInDb.Phone = item.Phone;
                studentInDb.Email = item.Email;
                context.Entry(studentInDb).State = EntityState.Modified;
                return await ContextSaver<StudentsRepository>.SaveAsync(context, logger);
            }
            else
                throw new StudentNotFoundException(id);
        }
    }
}
