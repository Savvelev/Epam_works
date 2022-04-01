using Domain;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("/api/lecture")]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService lectureService;

        public LectureController(ILectureService lectureService)
        {
            this.lectureService = lectureService;
        }

        [HttpGet]
        public async Task<IEnumerable<Lecture>> GetLectures()
        {
            return await lectureService.GetAllAsync();
        }


        [HttpGet("id")]
        public async Task<ActionResult<Lecture>> GetLecture(int id)
        {
            return await lectureService.GetByIdAsync(id) switch
            {
                null => NotFound(),
                var lecture => lecture
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddLecture(Lecture lecture)
        {
            var result = await lectureService.CreateAsync(lecture);
            return new ObjectResult(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateLecture(int id, Lecture lecture)
        {
            await lectureService.UpdateAsync(lecture, id);
            return Ok($"api/lecture/{id}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            return new ObjectResult(await lectureService.DeleteAsync(id));
        }
        [HttpGet("Report")]
        public async Task<string> ReportAttendance(string Name, FormatSourse format)
        {
            return await lectureService.Report(Name, format);
        }
    }
}
