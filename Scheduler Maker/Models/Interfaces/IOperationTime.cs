namespace SchedulerMaker.Models.Interfaces
{
    interface IOperationTime
    {
        int MachineToolId { get; }

        int NomenclatureId { get; }

        int ExecutionTime { get; }
    }
}
