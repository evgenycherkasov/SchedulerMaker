using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models
{
    class MachineToolClass : IMachineTool
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public MachineToolClass(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
