using DataAccess.DbEntities;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SeedTestData
    {
        public static async Task SeedLecture(UniversityDbContext context)
        {           
            await context.Attendances.AddRangeAsync(
                new AttendanceDb 
                { 
                    Id = 1,
                    Student = context.Students.FirstOrDefault(f => f.Id == 1), 
                    IsStudentAttended = true,
                    Homework = context.Homeworks.FirstOrDefault(h => h.Id == 1),
                    Lecture = context.Lectures.FirstOrDefault(l => l.Id == 1)
                },

                new AttendanceDb 
                { 
                    Id = 2, 
                    Student = context.Students.FirstOrDefault(f => f.Id == 1),
                    IsStudentAttended = false,
                    Homework = context.Homeworks.FirstOrDefault(h => h.Id == 2),
                    Lecture = context.Lectures.FirstOrDefault(l => l.Id == 1) 
                },

                new AttendanceDb 
                { 
                    Id = 3,
                    Student = context.Students.FirstOrDefault(f => f.Id == 2),
                    IsStudentAttended = true, 
                    Homework = context.Homeworks.FirstOrDefault(h => h.Id == 1),
                    Lecture = context.Lectures.FirstOrDefault(l => l.Id == 2) 
                },

                new AttendanceDb 
                {
                    Id = 4, 
                    Student = context.Students.FirstOrDefault(f => f.Id == 2),
                    IsStudentAttended = true,
                    Homework = context.Homeworks.FirstOrDefault(h => h.Id == 3),
                    Lecture = context.Lectures.FirstOrDefault(l => l.Id == 2) 
                }

                );
            await context.SaveChangesAsync();
        }
    }    
}


 