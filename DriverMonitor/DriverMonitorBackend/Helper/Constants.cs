namespace DriverMonitorBackend.Helper
{
    public class Constants
    {
        private const string VIOLATION_DRIVE = "Drive";
        private const string VIOLATION_REST = "Rest";
    }

    public enum ViolationType
    {
        SingleDrive,
        SingleRest,
        DayDrive,
        WeekDrive,
        NoViolation
    }
}
