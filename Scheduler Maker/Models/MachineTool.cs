using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class MachineTool : IMachineTool
    {
        public uint Id { get; }

        public string Name { get; }

        public MachineTool(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
