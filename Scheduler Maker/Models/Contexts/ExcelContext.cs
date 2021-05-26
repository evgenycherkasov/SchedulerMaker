using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Contexts
{
    class ExcelContext<T> : IContext<T>
        where T : class
    {
        public IEnumerable<T> GetDataList()
        {
            throw new NotImplementedException();
        }

        public void ReadDataList(IEnumerable<T> data)
        {
            throw new NotImplementedException();
        }
    }
}
