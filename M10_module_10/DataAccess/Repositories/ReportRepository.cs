using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    internal class ReportRepository : IReportRepository
    {
        private readonly UniversityDbContext context;
        private readonly IMapper mapper;

        public ReportRepository(UniversityDbContext DbContext, IMapper mapper)
        {
            context = DbContext;
            this.mapper = mapper;
        }

        public IEnumerable<Lecture> GetLecture()
        {
            var lectureDb = context.Lectures
                    .ToList();
            return mapper.Map<IReadOnlyCollection<Lecture>>(lectureDb);
        }

        public IEnumerable<Student> GetStudent()
        {
            var studentDb = context.Students
                    .ToList();
            return mapper.Map<IReadOnlyCollection<Student>>(studentDb);
        }
    }
}
