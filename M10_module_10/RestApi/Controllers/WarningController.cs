using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("/api/warning")]
    public class WarningController : ControllerBase
    {
        private readonly IWarningService warningService;

        public WarningController(IWarningService warningService)
        {
            this.warningService = warningService;

        }

        [HttpPut("/EmailWarning")]
        public async Task<IActionResult> EmailWarning(string lastName, string firstName)
        {
            var result = await warningService.MarkCourseWarning(lastName, firstName);
            return new ObjectResult(result);
        }
        
        [HttpPut("/CourseMarkWarning")]
        public async Task<IActionResult> AttendanceWarning(string lastName, string firstName)
        {
            var result = await warningService.AttendanceWarning(lastName, firstName);
            return new ObjectResult(result);
        }
    }
}