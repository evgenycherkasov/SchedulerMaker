﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IPart
    {
        int Id { get; set; }

        int NomenclatureId { get; set; }
    }
}