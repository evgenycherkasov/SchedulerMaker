using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    public interface IScheduleRepository<ISchedule>
    {
        void UnloadData(IEnumerable<ISchedule> schedule);
    }
}
