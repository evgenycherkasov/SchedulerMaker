using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class NomenclaturesRepository : IRepository<INomenclature>
    {
        private IContext<INomenclature> _context = null;

        public NomenclaturesRepository(IContext<INomenclature> context)
        {
            _context = context;
        }

        public IEnumerable<INomenclature> GetDataList()
        {
            throw new NotImplementedException();
        }

        public void WriteDataList(IEnumerable<INomenclature> data)
        {
            throw new NotImplementedException();
        }
    }
}
