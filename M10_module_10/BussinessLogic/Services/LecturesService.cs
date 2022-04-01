using Domain;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions.NotFound;

namespace BussinessLogic.Services
{
    public class LecturesService : ILectureService
    {

        private readonly ILectureRepository lectureRepository;
        private readonly ILogger<LecturesService> logger;
        private readonly Func<FormatSourse, IFormatReport> formatReport;

        public LecturesService(ILectureRepository lectureRepository, ILogger<LecturesService> logger, Func<FormatSourse,IFormatReport> formatReport)
        {
            this.lectureRepository = lectureRepository;
            this.logger = logger;
            this.formatReport = formatReport;
            this.logger.Log(LogLevel.Information, "LecturesService starts work");
        }

        public async Task<Lecture> GetByIdAsync(int id)
        {
            logger.Log(LogLevel.Information, "GetByIdAsync");
            return await lectureRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Lecture>> GetAllAsync()
        {
            logger.Log(LogLevel.Information, "GetAllAsync");
            return await lectureRepository.GetAllAsync();
        }

        public async Task<bool> CreateAsync(Lecture item)
        {
            logger.Log(LogLevel.Information, "CreateAsync");
            return await lectureRepository.CreateAsync(item);
        }

        public async Task<bool> UpdateAsync(Lecture item, int id)
        {
            logger.Log(LogLevel.Information, "UpdateAsync");
            return await lectureRepository.UpdateAsync(item, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            logger.Log(LogLevel.Information, "DeleteAsync");
            return await lectureRepository.DeleteAsync(id);
        }

        public async Task<string> Report(string name, FormatSourse sourse)
        {
            var lecturesList = await lectureRepository.GetLecturesIncludeStudentAsync();
            Lecture lecture = lecturesList.FirstOrDefault(l => l.Name == name);
            if (lecture == null)
                throw new LectureNotFoundException(name);

            var reports = new List<ReportEntity>();
         
            foreach (var attendingLecture in lecture.Attendances)
            {
                reports.Add(new ReportEntity()
                {
                    Lecture = lecture.Name,
                    StudentName = $"{attendingLecture.Student.FirstName} {attendingLecture.Student.LastName}",
                    AttendingLecture = attendingLecture.IsStudentAttended
                });
            }
            return await formatReport(sourse).WriteReportAsync(reports);
        }
    }
}
