using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    public class LectorsService : ILectorService
    {

        private readonly ILectorRepository lectorRepository;
        private readonly ILogger<LectorsService> logger;

        public LectorsService(ILectorRepository lectorRepository, ILogger<LectorsService> logger)
        {
            this.lectorRepository = lectorRepository;
            this.logger = logger;
            this.logger.Log(LogLevel.Information, "LectorsService starts work");
        }

        public async Task<Lector> GetByIdAsync(int id)
        {
            logger.Log(LogLevel.Information, "GetByIdAsync");
            return await lectorRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Lector>> GetAllAsync()
        {
            logger.Log(LogLevel.Information, "GetAllAsync");
            return await lectorRepository.GetAllAsync();
        }

        public async Task<bool> CreateAsync(Lector item)
        {
            logger.Log(LogLevel.Information, "CreateAsync");
            return await lectorRepository.CreateAsync(item);
        }

        public async Task<bool> UpdateAsync(Lector item, int id)
        {
            logger.Log(LogLevel.Information, "UpdateAsync");
            return await lectorRepository.UpdateAsync(item, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            logger.Log(LogLevel.Information, "DeleteAsync");
            return await lectorRepository.DeleteAsync(id);
        }
    }
}
