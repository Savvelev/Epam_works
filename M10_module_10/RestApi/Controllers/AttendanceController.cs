using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("/api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService attendanceSevice;

        public AttendanceController(IAttendanceService attendanceSevice)
        {
            this.attendanceSevice = attendanceSevice;
        }

        [HttpGet]
        public async Task<IEnumerable<Attendance>> GetAllAttendances()
        {
            return await attendanceSevice.GetAllAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            return await attendanceSevice.GetByIdAsync(id) switch
            {
                null => NotFound(),
                var attendance => attendance
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateAttendance(Attendance attendance)
        {
            var result = await attendanceSevice.CreateAsync(attendance);
            return new ObjectResult(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateAttendance(int id, Attendance attendance)
        {
            await attendanceSevice.UpdateAsync(attendance, id);
            return Ok($"api/attendance/{id}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            return new ObjectResult(await attendanceSevice.DeleteAsync(id));
        }
    }
}
