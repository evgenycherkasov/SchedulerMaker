using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class PartRepository : IRepository<IPart>
    {
        private IContext<IPart> _context = null;

        public PartRepository(IContext<IPart> context)
        {
            _context = context;
        }

        public IEnumerable<IPart> GetDataList()
        {
            throw new NotImplementedException();
        }

        public void WriteDataList(IEnumerable<IPart> data)
        {
            throw new NotImplementedException();
        }
    }
}
