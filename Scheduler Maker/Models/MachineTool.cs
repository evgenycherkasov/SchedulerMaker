using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    class MachineTool : IMachineTool
    {
        public int Id { get; }

        public string Name { get; }

        public MachineTool(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
