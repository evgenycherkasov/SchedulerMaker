namespace SchedulerMaker.Models.Interfaces
{
    public interface ISchedule
    {
        IPart Part { get; }

        IMachineTool MachineTool { get; }

        int StartTime { get; }

        int EndTime { get; }
    }
}
