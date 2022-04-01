using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    internal class SaveContext<T>
    {
        public static async Task<bool> Save(UniversityDbContext context, ILogger<T> logger)
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, "Some problems with saving content", e.Message, e.StackTrace);
                throw;
            }
            finally { await context.DisposeAsync(); }
        }
    }
}
