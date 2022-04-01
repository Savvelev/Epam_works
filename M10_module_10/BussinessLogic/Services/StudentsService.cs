using Domain;
using Domain.Entities;
using Domain.Exceptions.NotFound;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class StudentsService : IStudentService
    {

        private readonly IStudentRepository studentRepository;
        private readonly ILogger<StudentsService> logger;
        private readonly Func<FormatSourse, IFormatReport> formatReport;

        public StudentsService(IStudentRepository studentRepository, ILogger<StudentsService> logger, Func<FormatSourse, IFormatReport> formatReport)
        {
            this.studentRepository = studentRepository;
            this.logger = logger;
            this.formatReport = formatReport;
            this.logger.Log(LogLevel.Information, "StudentsService starts work");
        }

        public async Task<bool> AddStudentToAttendance(int studentId, int attendanceId)
        {
            return await studentRepository.AddStudentToAttendance(studentId, attendanceId);
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            logger.Log(LogLevel.Information, "GetByIdAsync");
            return await studentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            logger.Log(LogLevel.Information, "GetAllAsync");
            return await studentRepository.GetAllAsync();
        }

        public async Task<bool> CreateAsync(Student item)
        {
            logger.Log(LogLevel.Information, "CreateAsync");
            return await studentRepository.CreateAsync(item);
        }

        public async Task<bool> UpdateAsync(Student item, int id)
        {
            logger.Log(LogLevel.Information, "UpdateAsync");
            return await studentRepository.UpdateAsync(item, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            logger.Log(LogLevel.Information, "DeleteAsync");
            return await studentRepository.DeleteAsync(id);
        }

        public async Task<string> Report(string FirstName, string LastName, FormatSourse sourse)
        {
            var studentsList = await studentRepository.GetStudentsIncludeLectureAsync();
            Student students = studentsList.FirstOrDefault(student => student.LastName == LastName && student.FirstName == FirstName);
            if (students == null)
                throw new StudentNotFoundException(FirstName, LastName);

            var reports = new List<ReportEntity>();

            foreach (var attendingLecture in students.Attendances)
            {
                reports.Add(new ReportEntity()
                {
                    Lecture = attendingLecture.Lecture.Name,
                    StudentName = $"{students.FirstName} {students.LastName}",
                    AttendingLecture = attendingLecture.IsStudentAttended
                });
            }

            return await formatReport(sourse).WriteReportAsync(reports);
        }
    }
}


