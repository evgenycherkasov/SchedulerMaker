using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class Nomenclature : INomenclature
    {
        public uint Id { get; }

        public string Name { get; }

        public Nomenclature(uint id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
