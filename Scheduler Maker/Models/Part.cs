using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Models
{
    class Part : IPart
    {
        public int Id { get; set; }

        public int NomenclatureId { get; set; }

        public Part(int id, int nomenclatureId)
        {
            Id = id;
            NomenclatureId = nomenclatureId;
        }
    }
}
