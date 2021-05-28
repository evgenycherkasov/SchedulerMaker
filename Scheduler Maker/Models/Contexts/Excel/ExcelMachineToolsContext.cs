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
    class ExcelMachineToolsContext
    {
        public DataSet MachineToolsDataSet { get; set; }

        private MachineToolsRepository _repository;

        public ExcelMachineToolsContext(string machineToolsPath)
        {
            UploadMachineToolsDataSet(machineToolsPath);
            _repository = new MachineToolsRepository(this);
        }

        private void UploadMachineToolsDataSet(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    MachineToolsDataSet = result;
                }
            }
        }

        public IEnumerable<IMachineTool> GetDataList()
        {
            return _repository.GetDataList();
        }
    }
}
