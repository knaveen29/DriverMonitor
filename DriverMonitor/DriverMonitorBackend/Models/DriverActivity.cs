namespace DriverMonitorBackend.Models
{
    public class DriverActivity
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string ActivityType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
