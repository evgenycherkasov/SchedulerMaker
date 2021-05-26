using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IMachineTool
    {
        int Id { get; set; }

        string Name { get; set; }
    }
}
