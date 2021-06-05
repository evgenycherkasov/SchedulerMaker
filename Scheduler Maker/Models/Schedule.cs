using SchedulerMaker.Models.Interfaces;
using System;

namespace SchedulerMaker.Models
{
    public class Schedule : ISchedule
    {
        public IPart Part { get; }

        public IMachineTool MachineTool { get; }

        public uint StartTime { get; }

        public uint EndTime { get; }

        public Schedule(IPart part, IMachineTool machineTool, uint startTime, uint endTime)
        {
            Part = part;
            MachineTool = machineTool;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
