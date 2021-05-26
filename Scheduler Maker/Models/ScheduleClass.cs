using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models
{
    class ScheduleClass : ISchedule
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IPart Part { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IMachineTool MachineTool { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int StartTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int EndTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
