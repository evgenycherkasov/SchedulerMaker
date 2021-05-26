using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchedulerMaker.Models.Interfaces;


namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class MachineToolsRepository : IRepository<IMachineTool>
    {
        private IContext<IOperationTime> _context = null;

        public MachineToolsRepository(IContext<IOperationTime> context)
        {
            _context = context;
        }

        public IEnumerable<IMachineTool> GetDataList()
        {
            throw new NotImplementedException();
        }

        public void WriteDataList(IEnumerable<IMachineTool> data)
        {
            throw new NotImplementedException();
        }
    }
}
