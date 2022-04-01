using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Controllers
{
    [ApiController]
    [Route("/api/homework")]
    public class HomeworkController : ControllerBase
    {
        private readonly IHomeworkService homeworkSevice;


        public HomeworkController(IHomeworkService homeworkSevice)
        {
            this.homeworkSevice = homeworkSevice;

        }

        [HttpGet]
        public async Task<IEnumerable<Homework>> GetAllHomeworks()
        {
            return await homeworkSevice.GetAllAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Homework>> GetHomework(int id)
        {
            return await homeworkSevice.GetByIdAsync(id) switch
            {
                null => NotFound(),
                var homework => homework
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateHomework(Homework homework)
        {
            var result = await homeworkSevice.CreateAsync(homework);
            return new ObjectResult(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateHomework(int id, Homework homework)
        {
            await homeworkSevice.UpdateAsync(homework, id);
            return Ok($"api/homework/{id}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHomework(int id)
        {
            return new ObjectResult(await homeworkSevice.DeleteAsync(id));
        }
    }
}
