namespace SchedulerMaker.Models.Interfaces
{
    interface ISchedule
    {
        IPart Part { get; }

        IMachineTool MachineTool { get; }

        int StartTime { get; }

        int EndTime { get; }
    }
}
