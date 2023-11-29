using DriverMonitorBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace DriverMonitorBackend.Services
{
    public interface IDriverActivityService
    {
        Task<ActionResult<List<DriverActivityFile>>> GetAllActivity(DateTime startDate, DateTime endDate);
        Task<DriverActivityResponse> GetDriversActivity(DateTime startDate, DateTime endDate);

        Task PostDriverFile(DriverActivityFile files);

        Task PostDriverFiles(List<DriverActivityFile> files);

        Task<DriverActivityValidationResponse> ValidateFiles(DateTime simulationDate, List<DriverActivityFile> files);
    }
}