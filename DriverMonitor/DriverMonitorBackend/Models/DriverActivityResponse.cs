namespace DriverMonitorBackend.Models
{
    public class DriverActivityResponse
    {
        public List<DriverActivityViolation> Violations { get; set; } = new List<DriverActivityViolation>();   
    }
}
