using DriverMonitorBackend.Business;
using DriverMonitorBackend.Models;
using DriverMonitorBackend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DriverMonitorBackend.Services
{
    public class DriverActivityService : IDriverActivityService
    {
        private readonly IDriverActivityRepository _driverActivityRepository;
        private readonly ILogger<ApiDBContext> _logger;

        public DriverActivityService(IDriverActivityRepository driverActivityRepository, ILogger<ApiDBContext> logger)
        {
            _driverActivityRepository = driverActivityRepository;
            _logger = logger;
        }


        /// <summary>
        /// Get drivers activity violation data for given start and end date 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<DriverActivityResponse> GetDriversActivity(DateTime startDate, DateTime endDate)
        {
            var activities = await _driverActivityRepository.GetAsync(startDate, endDate);

            _logger.LogInformation("Validating the activity data and transform them into violations");

            var result = GetViolationDataFromActivities(activities);

            return new DriverActivityResponse() { Violations = result };
        }


        /// <summary>
        /// Adds a activity record
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public Task PostDriverFile(DriverActivityFile files)
        {
            return _driverActivityRepository.PostFileAsync(files);
        }

        /// <summary>
        /// Adds a list of activity records
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public Task PostDriverFiles(List<DriverActivityFile> files)
        {
            return _driverActivityRepository.PostFilesAsync(files);
        }

        /// <summary>
        /// Convert activities records into violation entity by applyin the logic
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private List<DriverActivityViolation> GetViolationDataFromActivities(IEnumerable<DriverActivityFile> activities)
        {
            //iterate through each violation type against the data and extract the data into based on their violation
            var violations = Enum.GetNames(typeof(Helper.ViolationType));
            var result = new List<DriverActivityViolation>();

            foreach (var violation in violations)
            {
                var violationFactory = new ViolationFactory().GetViolationType(violation);
                var violationData = violationFactory?.GetViolationData(activities.ToList());
                if(violationData != null)
                    result.Add(violationData);
            }

            return result;
        }

        /// <summary>
        /// Validate the list of simulated activites for the given date
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<DriverActivityValidationResponse> ValidateFiles(DateTime creationDate, List<DriverActivityFile> files)
        {
            var existingFilesForDate = await _driverActivityRepository.GetDataByCreationDateAsync(creationDate);

            var filesToUploadByDrivers = files.GroupBy(_ => _.DriverId);

            var invalidFiles = new List<DriverActivityFile>();

            return DoValidation(files, existingFilesForDate, filesToUploadByDrivers, invalidFiles);

        }

        private DriverActivityValidationResponse DoValidation(
            List<DriverActivityFile> files, 
            IEnumerable<DriverActivityFile> existingFilesForDate, 
            IEnumerable<IGrouping<int, DriverActivityFile>> filesToUploadByDrivers, 
            List<DriverActivityFile> invalidFiles)
        {
            _logger.LogInformation("Validating input files against existing files");

            var result = ValidateDriverCount(filesToUploadByDrivers, files);

            if (!result.IsValidSimulation)
                return result;

            result = ValidateFileCountForTheDay(files, existingFilesForDate);
            if (!result.IsValidSimulation)
                return result;

            foreach (var driver in filesToUploadByDrivers)
            {
                var filesInDBForDriver = existingFilesForDate.Where(_ => _.DriverId == driver.Key).ToList();

                //TODO: need to finetune this logic to find out the overlapping time range.
                //currently it checks for exact match of records
                var isExisting = filesInDBForDriver.Any(_ => 
                driver.Any(a => a.StartTime == _.StartTime) 
                || driver.Any(a => a.EndTime == _.EndTime));

                if (isExisting)
                {
                    invalidFiles.AddRange(driver.ToList());

                    return new DriverActivityValidationResponse()
                    {
                        IsValidSimulation = false,
                        ValidationFailureType = 3,
                        DriverActivityFiles = invalidFiles
                    };
                }
            }

            return new DriverActivityValidationResponse()
            {
                IsValidSimulation = true,
                ValidationFailureType = 0,
                DriverActivityFiles = files
            };

        }


        private DriverActivityValidationResponse ValidateDriverCount(IEnumerable<IGrouping<int, DriverActivityFile>> filesToUploadByDrivers, List<DriverActivityFile> files)
        {
            var driversCount = filesToUploadByDrivers.Count();

            if (driversCount > 30)
            {
                _logger.LogInformation($"Not valid! Total count of unique drivers {driversCount} are higher than the limit. ");

                return new DriverActivityValidationResponse()
                {
                    IsValidSimulation = false,
                    ValidationFailureType = 1,
                    DriverActivityFiles = files,
                };
            }

            return new DriverActivityValidationResponse() { IsValidSimulation = true };
        }


        private DriverActivityValidationResponse ValidateFileCountForTheDay(List<DriverActivityFile> files, IEnumerable<DriverActivityFile> existingFilesForDate)
        {
            var filesForDate = existingFilesForDate.Count();
            var filesCount = files.Count();

            if (filesForDate + filesCount > 100)
            {
                _logger.LogInformation($"Not valid! Total count of files for the given date {filesForDate + filesCount} are higher than the limit. ");

                return new DriverActivityValidationResponse()
                {
                    IsValidSimulation = false,
                    ValidationFailureType = 2,
                    DriverActivityFiles = files
                };
            }

            return new DriverActivityValidationResponse() { IsValidSimulation = true };
        }

        public async Task<ActionResult<List<DriverActivityFile>>> GetAllActivity(DateTime startDate, DateTime endDate)
        {
            var activites = await _driverActivityRepository.GetAllAsync(startDate, endDate);
            return activites.ToList();

        }
    }
}
