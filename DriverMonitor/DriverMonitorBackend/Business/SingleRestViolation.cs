using DriverMonitorBackend.Helper;
using DriverMonitorBackend.Models;

namespace DriverMonitorBackend.Business
{
    public class SingleRestViolation : IViolationFactory
    {
        private readonly DriverActivityViolation violation = new DriverActivityViolation();
        private readonly ViolationType violationType = ViolationType.SingleRest;

        public SingleRestViolation()
        {
            violation.Type = violationType;
        }

        public DriverActivityViolation GetViolationData(List<DriverActivityFile> activities)
        {
            foreach (var activity in activities.Where(_ => _.Activity == "Rest").ToList())
            {
                var violationActivity = new DriverActivity();

                if ((activity.EndTime - activity.StartTime).TotalMinutes < 45)
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
