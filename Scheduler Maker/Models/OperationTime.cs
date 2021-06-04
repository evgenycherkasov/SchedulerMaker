using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class OperationTime : IOperationTime
    {
        public int MachineToolId { get; }

        public int NomenclatureId { get; }

        public int ExecutionTime { get; }

        public OperationTime(int mtId, int nomenclatureId, int executionTime)
        {
            MachineToolId = mtId;
            NomenclatureId = nomenclatureId;
            ExecutionTime = executionTime;
        }
    }
}
