using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models
{
    class PartClass : IPart
    {
        public int Id { get; set; }

        public int NomenclatureId { get; set; }

        public PartClass(int id, int nomenclatureId)
        {
            Id = id;
            NomenclatureId = nomenclatureId;
        }
    }
}
