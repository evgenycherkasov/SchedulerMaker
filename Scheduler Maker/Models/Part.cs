using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class Part : IPart
    {
        public uint Id { get; }

        public uint NomenclatureId { get; }

        public Part(uint id, uint nomenclatureId)
        {
            Id = id;
            NomenclatureId = nomenclatureId;
        }
    }
}
