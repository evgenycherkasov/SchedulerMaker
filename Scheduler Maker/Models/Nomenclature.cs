using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    class Nomenclature : INomenclature
    {
        public int Id { get; }

        public string Name { get; }

        public Nomenclature(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
