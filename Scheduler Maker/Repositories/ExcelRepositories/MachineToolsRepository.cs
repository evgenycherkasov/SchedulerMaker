﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using SchedulerMaker.Models;
using SchedulerMaker.Models.Interfaces;


namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class MachineToolsRepository : IRepository<IMachineTool>
    {
        private readonly string _idFieldName = "id";
        private readonly string _nomenclatureFieldName = "name";
        private readonly DataSet _machineTools = null;

        public MachineToolsRepository(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    _machineTools = result;
                }
            }
        }

        public IEnumerable<IMachineTool> GetDataList()
        {
            //TODO: обработать IOException
            DataSet machineToolsDataSet = _machineTools;
            ValidateDataSet(machineToolsDataSet);
            DataTable machineToolsTable = machineToolsDataSet.Tables[0];
            List<IMachineTool> machineToolsList = new List<IMachineTool>();
            for (int i = 1; i < machineToolsTable.Rows.Count; ++i)
            {
                DataRow row = machineToolsTable.Rows[i];
                int id = Convert.ToInt32(row[0]);
                string name = Convert.ToString(row[1]);
                MachineTool machineTool = new MachineTool(id, name);

                if (machineToolsList.Any((t) => t.Id == machineTool.Id))
                {
                    throw new ApplicationException("Есть совпадающие идентификаторы оборудования");
                }
                machineToolsList.Add(machineTool);
            }
            return machineToolsList;
        }

        public void WriteDataList(IEnumerable<IMachineTool> data, string path)
        {
            throw new NotImplementedException();
        }

        private void ValidateDataSet(DataSet data)
        {
            /*
             * Не проверяю на содержание хотя бы одного листа, так как
             * в Excel файле всегда как минимум один лист
             */
            if (data.Tables.Count > 1)
            {
                throw new ApplicationException("Слишком много листов в файле");
            }
            DataTable table = data.Tables[0];
            if (table.Columns.Count > 2)
            {
                throw new ApplicationException("Слишком много столбцов в таблице");
            }
            if (table.Rows.Count < 2)
            {
                throw new ApplicationException("Нет данных в таблице");
            }
            DataRow firstRow = table.Rows[0];

            if (firstRow[0].ToString() != _idFieldName || firstRow[1].ToString() != _nomenclatureFieldName)
            {
                throw new ApplicationException($"Не найдены соответствующие столбцы {_idFieldName} и {_nomenclatureFieldName} для партии");
            }
        }
    }
}
