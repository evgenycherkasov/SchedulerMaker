using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using ExcelDataReader.Exceptions;
using SchedulerMaker.Models;
using SchedulerMaker.Models.Interfaces;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    public class OperationTimesRepository : IDataRepository<IOperationTime>
    {
        private readonly string _machineToolIdFieldName = "machine tool id";
        private readonly string _nomenclatureIdFieldName = "nomenclature id";
        private readonly string _operationTimeFieldName = "operation time";

        private readonly DataSet _operationTimes = null;

        public OperationTimesRepository(string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        _operationTimes = result;
                    }
                }
            }
            catch (ExcelReaderException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (IOException ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<IOperationTime> GetDataList()
        {
            try
            {
                DataSet operationTimesDataSet = _operationTimes;
                ValidateDataSet(operationTimesDataSet);
                DataTable operationTimesTable = operationTimesDataSet.Tables[0];
                List<IOperationTime> operationTimesList = new List<IOperationTime>();
                for (int i = 1; i < operationTimesTable.Rows.Count; ++i)
                {
                    DataRow row = operationTimesTable.Rows[i];
                    uint mtId = Convert.ToUInt32(row[0]);
                    uint nomenclatureId = Convert.ToUInt32(row[1]);
                    uint executionTime = Convert.ToUInt32(row[2]);
                    IOperationTime operation = new OperationTime(mtId, nomenclatureId, executionTime);

                    operationTimesList.Add(operation);
                }
                return operationTimesList;
            }
            catch (InvalidCastException)
            {
                throw new ApplicationException("В одной из строк неверно указана операция");
            }
            catch (FormatException)
            {
                throw new ApplicationException("В одной из строк неверно указана операция");
            }
            catch (OverflowException)
            {
                throw new ApplicationException("В одной из строк неверно указана операция");
            }

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
            if (table.Columns.Count > 3)
            {
                throw new ApplicationException("Слишком много столбцов в таблице");
            }
            if (table.Rows.Count < 2)
            {
                throw new ApplicationException("Нет данных в таблице");
            }
            DataRow firstRow = table.Rows[0];

            if (firstRow[0].ToString() != _machineToolIdFieldName || firstRow[1].ToString() != _nomenclatureIdFieldName || firstRow[2].ToString() != _operationTimeFieldName)
            {
                throw new ApplicationException($"Не найдены соответствующие столбцы {_machineToolIdFieldName}, {_nomenclatureIdFieldName} и {_operationTimeFieldName} для партии");
            }
        }
    }
}
