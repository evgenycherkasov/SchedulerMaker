using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IRepository<IData>
    {
        IEnumerable<IData> GetDataList();

        void WriteDataList(IEnumerable<IData> data, string path);
    }
}
