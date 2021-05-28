using ExcelDataReader;
using SchedulerMaker.Models.Interfaces;
using SchedulerMaker.Repositories.ExcelRepositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Models.Contexts.Excel
{
    class ExcelPartiesContext
    {
        public DataSet PartiesDataSet { get; set; }

        private PartRepository _repository;

        public ExcelPartiesContext(string partiesPath)
        {
            UploadPartiesDataSet(partiesPath);
            _repository = new PartRepository(this);
        }

        private void UploadPartiesDataSet(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    PartiesDataSet = result;
                }
            }
        }

        public IEnumerable<IPart> GetDataList()
        {
            return _repository.GetDataList();
        }
    }
}
