using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BussinessLogic.Services
{
    class AttendancesService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;
        private readonly ILogger<AttendancesService> logger;

        public AttendancesService(IAttendanceRepository attendanceRepository, ILogger<AttendancesService> logger)
        {

            this.attendanceRepository = attendanceRepository;
            this.logger = logger;
            this.logger.Log(LogLevel.Information, "AttendancesService starts work");
        }

        public async Task<bool> CreateAsync(Attendance item)
        {
            logger.Log(LogLevel.Information, "CreateAsync");
            return await attendanceRepository.CreateAsync(item);
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            logger.Log(LogLevel.Information, "GetAllAsync");
            return await attendanceRepository.GetAllAsync();
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            logger.Log(LogLevel.Information, "GetByIdAsync");
            return await attendanceRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Attendance item, int id)
        {
            logger.Log(LogLevel.Information, "UpdateAsync");
            return await attendanceRepository.UpdateAsync(item, id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            logger.Log(LogLevel.Information, "DeleteAsync");
            return await attendanceRepository.DeleteAsync(id);
        }
    }
}
