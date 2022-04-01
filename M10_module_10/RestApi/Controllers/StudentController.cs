using Domain;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("/api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;


        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPut("/addStudentToLecture")]
        public async Task<IActionResult> AddStudentToAttendance(int StudentId, int AttendanceId)
        {
            var result = await studentService.AddStudentToAttendance(StudentId, AttendanceId);
            return new ObjectResult(result); 
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await studentService.GetAllAsync();
        }


        [HttpGet("id")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            return await studentService.GetByIdAsync(id) switch
            {
                null => NotFound(),
                var student => student
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            var result = await studentService.CreateAsync(student);
            return new ObjectResult(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            await studentService.UpdateAsync(student, id);
            return Ok($"api/lecture/{id}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return new ObjectResult(await studentService.DeleteAsync(id));
        }

        [HttpGet("Report")]
        public async Task<string> ReportAttendance(string FirstName, string LastName, FormatSourse format)
        {
            return await studentService.Report(FirstName, LastName, format);
        }
    }
}
