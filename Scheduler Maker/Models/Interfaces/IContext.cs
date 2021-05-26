using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Interfaces
{
    interface IContext<T>
        where T : class
    {
        IEnumerable<T> GetDataList();

        void ReadDataList(IEnumerable<T> data);
    }
}
