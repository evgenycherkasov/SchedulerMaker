namespace SchedulerMaker.Models.Interfaces
{
    public interface IOperationTime
    {
        int MachineToolId { get; }

        int NomenclatureId { get; }

        int ExecutionTime { get; }
    }
}
