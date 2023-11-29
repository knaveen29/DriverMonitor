using DriverMonitorBackend.Models;

namespace DriverMonitorBackend.Business
{
    public interface IViolationFactory
    {
        DriverActivityViolation GetViolationData(List<DriverActivityFile> activities);
    }

    public class ViolationFactory : IViolationFactory
    {
        public ViolationFactory() { }

        public IViolationFactory GetViolationType(string type)
        {
            switch (type) 
            {
                case "SingleDrive":
                    return new SingleDriveViolation();
                case "SingleRest":
                    return new SingleRestViolation();
                case "DayDrive":
                    return new DayDriveViolation();
                case "WeekDrive":
                    return new WeekDriveViolation();
            }

            return null;
        }

        public DriverActivityViolation GetViolationData(List<DriverActivityFile> activities)
        {
            throw new NotImplementedException();
        }
    }
}
