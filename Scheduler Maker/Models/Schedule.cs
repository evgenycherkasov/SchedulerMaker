using SchedulerMaker.Models.Interfaces;
using System;

namespace SchedulerMaker.Models
{
    class Schedule : ISchedule
    {
        public IPart Part { get; }

        public IMachineTool MachineTool { get; }

        public int StartTime { get; }

        public int EndTime { get; }

        public Schedule(IPart part, IMachineTool machineTool, int startTime, int endTime)
        {
            Part = part;
            MachineTool = machineTool;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
