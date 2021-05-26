using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface ISchedule
    {
        int Id { get; set; }

        IPart Part { get; set; }

        IMachineTool MachineTool { get; set; }

        int StartTime { get; set; }

        int EndTime { get; set; }
    }
}
