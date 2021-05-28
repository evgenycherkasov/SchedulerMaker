using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models
{
    class NomenclatureClass : INomenclature
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public NomenclatureClass(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
