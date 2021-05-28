using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IOperationTime : IData
    {
        int MachineToolId { get; set; }

        int NomenclatureId { get; set; }

        int OperationTime { get; set; }
    }
}
