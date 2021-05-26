using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetDataList();

        void WriteDataList(IEnumerable<T> data);
    }
}
