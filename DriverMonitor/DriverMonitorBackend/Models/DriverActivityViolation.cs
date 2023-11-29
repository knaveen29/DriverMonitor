using DriverMonitorBackend.Helper;

namespace DriverMonitorBackend.Models
{
    public class DriverActivityViolation
    {
        public ViolationType Type { get; set; }
        public List<DriverActivity> Activity { get; set; }  = new List<DriverActivity>();
    }
}
