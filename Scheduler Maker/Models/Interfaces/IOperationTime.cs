namespace SchedulerMaker.Models.Interfaces
{
    public interface IOperationTime
    {
        uint MachineToolId { get; }

        uint NomenclatureId { get; }

        uint ExecutionTime { get; }
    }
}
