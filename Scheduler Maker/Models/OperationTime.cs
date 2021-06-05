using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class OperationTime : IOperationTime
    {
        public uint MachineToolId { get; }

        public uint NomenclatureId { get; }

        public uint ExecutionTime { get; }

        public OperationTime(uint mtId, uint nomenclatureId, uint executionTime)
        {
            MachineToolId = mtId;
            NomenclatureId = nomenclatureId;
            ExecutionTime = executionTime;
        }
    }
}
