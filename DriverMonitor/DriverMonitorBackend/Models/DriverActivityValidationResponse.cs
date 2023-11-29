namespace DriverMonitorBackend.Models
{
    public class DriverActivityValidationResponse
    {
        public bool IsValidSimulation { get; set; }

        /// <summary>
        /// 0. No failure (validation success)
        /// 1. Drivers count > 30
        /// 2. Files count > 100 for the day
        /// 3. Overlapping time for driver
        /// </summary>
        public int ValidationFailureType { get; set; }
        public List<DriverActivityFile> DriverActivityFiles { get; set; }
    }
}
