using DriverMonitorBackend.Helper;
using DriverMonitorBackend.Models;

namespace DriverMonitorBackend.Business
{
    public class WeekDriveViolation : IViolationFactory
    {
        private readonly DriverActivityViolation violation = new DriverActivityViolation();
        private readonly ViolationType violationType = ViolationType.WeekDrive;

        public WeekDriveViolation()
        {
            violation.Type = violationType;
        }

        public DriverActivityViolation GetViolationData(List<DriverActivityFile> activities)
        {
            //var driverActivities = activities.GroupBy(_ => _.DriverId).ToList();
            //foreach (var activity in driverActivities)
            //{
            //    var violationActivity = new DriverActivity();

            //   if(activity.ToList().Sum(_ => _.EndTime.Hour) - activity.ToList().Sum(_ => _.StartTime.Hour) > 60)
            //    violationActivity.ActivityType = activity.Activity;
            //    violationActivity.DriverId = activity.DriverId;
            //    violationActivity.EndTime = activity.EndTime;
            //    violationActivity.StartTime = activity.StartTime;
            //    violationActivity.CreateTime = activity.CreateTime;
            //    violationActivity.Id = activity.Id;

            //    violation.Activity.Add(violationActivity);
            //}

            return violation;
        }
    }
}
