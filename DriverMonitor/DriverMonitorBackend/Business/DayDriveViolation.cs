using DriverMonitorBackend.Helper;
using DriverMonitorBackend.Models;

namespace DriverMonitorBackend.Business
{
    public class DayDriveViolation : IViolationFactory
    {
        private readonly DriverActivityViolation violation = new DriverActivityViolation();
        private readonly ViolationType violationType = ViolationType.DayDrive;

        public DayDriveViolation()
        {
            violation.Type = violationType;
        }

        public DriverActivityViolation GetViolationData(List<DriverActivityFile> activities)
        {
            foreach (var activity in activities.Where(_ => _.Activity == "Drive").ToList())
            {
                var violationActivity = new DriverActivity();

                if ((activity.EndTime - activity.StartTime).TotalHours > 12)
                {
                    violationActivity.ActivityType = activity.Activity;
                    violationActivity.DriverId = activity.DriverId;
                    violationActivity.EndTime = activity.EndTime;
                    violationActivity.StartTime = activity.StartTime;
                    violationActivity.CreateTime = activity.CreateTime;
                    violationActivity.Id = activity.Id;

                    violation.Activity.Add(violationActivity);
                }

            }

            return violation;
        }
    }
}
