using DriverMonitorBackend.Models;
using DriverMonitorBackend.Repository;
using DriverMonitorBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriverMonitorBackend
{
    /// <summary>
    /// Api for handling driver activity data.
    /// </summary>
    [ApiController]
    [Route("api/driver-activity/")]
    public class DriverActivityController : ControllerBase
    {
        private readonly ILogger<DriverActivityController> _logger;
        private readonly IDriverActivityService _driverActivityService;

        public DriverActivityController(ILogger<DriverActivityController> logger, IDriverActivityService driverActivityService)
        {
            _logger = logger;
            _driverActivityService = driverActivityService;
        }

        /// <summary>
        /// Gets the drivers' activity files between a given date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-activity")]
        public async Task<ActionResult<DriverActivityResponse>> GetDriversActivity(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Received request for getting driver activity - start {startDate} and end {endDate}");
            var result = await _driverActivityService.GetDriversActivity(startDate, endDate);
            return result;
        }

        /// <summary>
        /// Gets the drivers' activity files between a given date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-all-activity")]
        public async Task<ActionResult<List<DriverActivityFile>>> GetAllActivity(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"Received request for getting driver activity - start {startDate} and end {endDate}");
            var result = await _driverActivityService.GetAllActivity(startDate, endDate);
            return result;
        }

        /// <summary>
        /// Posts a single driver activity file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns Success/Failure status code</returns>
        [HttpPost]
        [Route("post-file")]
        public async Task<ActionResult> PostDriverFile([FromBody] DriverActivityFile file)
        {
            await _driverActivityService.PostDriverFile(file);
            return Ok();
        }

        /// <summary>
        /// Posts a list of driver activity file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Returns Success/Failure status code</returns>
        [HttpPost]
        [Route("post-files")]
        public async Task<ActionResult> PostDriverFiles([FromBody] List<DriverActivityFile> files)
        {
            await _driverActivityService.PostDriverFiles(files);
            return Ok();
        }

        /// <summary>
        /// Validates the list of simulated driver activity files for a given simulation date.
        /// </summary>
        /// <param name="simulationDate">The simulation date for validation.</param>
        /// <param name="files">The list of driver activity files to validate.</param>
        /// <returns>Validation response if its valid or not and list of files validated</returns>
        [HttpPost]
        [Route("validate-activities")]
        public async Task<ActionResult<DriverActivityValidationResponse>> PostValidateActivity(DateTime simulationDate, [FromBody] List<DriverActivityFile> files)
        {
            var result = await _driverActivityService.ValidateFiles(simulationDate, files);
            return Ok(result);
        }
    }
}