namespace SchedulerMaker.Models.Interfaces
{
    public interface ISchedule
    {
        IPart Part { get; }

        IMachineTool MachineTool { get; }

        uint StartTime { get; }

        uint EndTime { get; }
    }
}
