using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    public class Part : IPart
    {
        public int Id { get; }

        public int NomenclatureId { get; }

        public Part(int id, int nomenclatureId)
        {
            Id = id;
            NomenclatureId = nomenclatureId;
        }
    }
}
