using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApi.Controllers
{

    [ApiController]
    [Route("/api/lector")]
    public class LectorController : ControllerBase
    {
        private readonly ILectorService lectorService;

        public LectorController(ILectorService lectorService)
        {
            this.lectorService = lectorService;
        }


        [HttpGet]
        public async Task<IEnumerable<Lector>> GetLectors()
        {
            return await lectorService.GetAllAsync();
        }


        [HttpGet("id")]
        public async Task<ActionResult<Lector>> GetLector(int id)
        {
            return await lectorService.GetByIdAsync(id) switch
            {
                null => NotFound(),
                var lector => lector
            };
        }

        [HttpPost]
        public async Task<IActionResult> AddLector(Lector lector)
        {
            var result = await lectorService.CreateAsync(lector);
            return new ObjectResult(result);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateLector(int id, Lector lector)
        {
            await lectorService.UpdateAsync(lector, id);
            return Ok($"api/lector/{id}");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLector(int id)
        {
            return new ObjectResult(await lectorService.DeleteAsync(id));
        }
    }
}
