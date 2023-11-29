using DriverMonitorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverMonitorBackend.Repository
{
    public interface IDriverActivityRepository
    {
        Task<IEnumerable<DriverActivityFile>> GetAllAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<DriverActivityFile>> GetAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<DriverActivityFile>> GetDataByCreationDateAsync(DateTime creationDate);
        Task PostFileAsync(DriverActivityFile file);

        Task PostFilesAsync(List<DriverActivityFile> files);
    }


    public class DriverActivityRepository : IDriverActivityRepository
    {
        protected ApiDBContext _dbContext;
        private readonly ILogger<ApiDBContext> _logger;

        public DriverActivityRepository(ApiDBContext dbContext, ILogger<ApiDBContext> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<DriverActivityFile>> GetAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbContext.DriverActivityFiles
                   .Where(_ => _.CreateTime >= startDate && _.CreateTime <= endDate)
                   .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting driver activity data {ex}", ex.StackTrace);
                throw;
            }
        }

        public async Task<IEnumerable<DriverActivityFile>> GetDataByCreationDateAsync(DateTime creationDate)
        {
            try
            {
                return await _dbContext.DriverActivityFiles.Where(_ => _.CreateTime.Date == creationDate.Date).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting driver activity data for creation date {ex}", ex.StackTrace);
                throw;
            }
        }

        public async Task PostFileAsync(DriverActivityFile files)
        {
            try
            {
                await _dbContext.DriverActivityFiles.AddAsync(files);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in saving driver activity data {ex}", ex.StackTrace);
                throw;
            }
        }

        public async Task PostFilesAsync(List<DriverActivityFile> files)
        {
            try
            {
                await _dbContext.DriverActivityFiles.AddRangeAsync(files);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in saving driver activity data {ex}", ex.StackTrace);
                throw;
            }
        }

        public async Task<IEnumerable<DriverActivityFile>> GetAllAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _dbContext.DriverActivityFiles
                    .Where(_ => _.CreateTime >= startDate && _.CreateTime <= endDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in getting activity data {ex}", ex.StackTrace);
                throw;
            }
        }

    }
}
