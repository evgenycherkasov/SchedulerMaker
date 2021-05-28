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
    class ExcelNomenclaturesContext
    {
        public DataSet NomenclaturesDataSet { get; set; }

        private NomenclaturesRepository _repository;

        public ExcelNomenclaturesContext(string nomenclaturesPath)
        {
            UploadNomenclaturesDataSet(nomenclaturesPath);
            _repository = new NomenclaturesRepository(this);
        }

        private void UploadNomenclaturesDataSet(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    NomenclaturesDataSet = result;
                }
            }
        }

        public IEnumerable<INomenclature> GetDataList()
        {
            return _repository.GetDataList();
        }
    }
}
