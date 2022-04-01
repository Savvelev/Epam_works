using Domain;
using Domain.Exceptions.NotFound;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    class WarningService : IWarningService
    {
        private readonly IStudentRepository studentRepository;

        public WarningService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<bool> MarkCourseWarning(string LastName, string FirstName)
        {
            var countOfAttendances = 0;
            var sumOfMarks = 0;

            var studentlist = await studentRepository.GetStudentsIncludeLectureAsync();
            var student = studentlist.FirstOrDefault(student => student.LastName == LastName && student.FirstName == FirstName);
            if (student == null)
                throw new StudentNotFoundException(FirstName, LastName);
            foreach (var item in student.Attendances)
            {
                countOfAttendances++;
                if (!item.IsStudentAttended || item.Homework.CourseMark == 0)
                    continue;

                sumOfMarks += item.Homework.CourseMark;
            }
            if ((sumOfMarks / countOfAttendances) < Constant.LimitMark)
            {
                SendSmsWarning(student.Phone);
                return true;
            }
            else return false;
        }

        private void SendSmsWarning(string phoneNumber)
        {
        }



        public async Task<bool> AttendanceWarning(string LastName, string FirstName)
        {
            var studentlist = await studentRepository.GetStudentsIncludeLectureAsync();

            var student = studentlist.FirstOrDefault(student => student.LastName == LastName && student.FirstName == FirstName);
            if (student == null)
                throw new StudentNotFoundException(FirstName, LastName);

            var countOfAttendance = student.Attendances.Sum(statt => statt.IsStudentAttended ? 0 : 1);
            if (countOfAttendance <= Constant.LimitLecture)
            {
                SendEmailToLector();
                return true;
            }
            else return false;

        }

        private void SendEmailToLector()
        {
        }
    }
}
