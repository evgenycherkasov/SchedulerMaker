using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class OperationTimesRepository : IRepository<IOperationTime>
    {
        public IEnumerable<IOperationTime> GetDataList()
        {
            throw new NotImplementedException();
        }

        public void WriteDataList(IEnumerable<IOperationTime> data, string path)
        {
            throw new NotImplementedException();
        }
    }
}
