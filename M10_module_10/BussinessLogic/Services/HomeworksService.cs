using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    internal class HomeworksService : IHomeworkService
    {

        private readonly IHomeworkRepository homeworksRepository;
        private readonly ILogger<HomeworksService> logger;

        public HomeworksService(IHomeworkRepository homeworksRepository, ILogger<HomeworksService> logger)
        {
            this.homeworksRepository = homeworksRepository;
            this.logger = logger;
            this.logger.Log(LogLevel.Information, "HomeworkService starts work");
        }

        public async Task<bool> CreateAsync(Homework item)
        {
            logger.Log(LogLevel.Information, "CreateAsync");
            return await homeworksRepository.CreateAsync(item);
        }

        public async Task<IEnumerable<Homework>> GetAllAsync()
        {
            logger.Log(LogLevel.Information, "GetAllAsync");
            return await homeworksRepository.GetAllAsync();
        }

        public async Task<Homework> GetByIdAsync(int id)
        {
            logger.Log(LogLevel.Information, "GetByIdAsync");
            return await homeworksRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Homework item, int id)
        {
            logger.Log(LogLevel.Information, "UpdateAsync");
            return await homeworksRepository.UpdateAsync(item, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            logger.Log(LogLevel.Information, "DeleteAsync");
            return await homeworksRepository.DeleteAsync(id);
        }
    }
}
