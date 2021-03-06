using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ExcelDataReader;
using System.IO;
using SchedulerMaker.Models;
using ExcelDataReader.Exceptions;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    public class PartRepository : IDataRepository<IPart>
    {
        private readonly string _idFieldName = "id";
        private readonly string _nomenclatureIdFieldName = "nomenclature id";
        private readonly DataSet _parties = null;

        public PartRepository(string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        _parties = result;
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

        public IEnumerable<IPart> GetDataList()
        {
            try
            {
                DataSet partiesDataSet = _parties;
                ValidateDataSet(partiesDataSet);
                DataTable partiesTable = partiesDataSet.Tables[0];
                List<IPart> partiesList = new List<IPart>();
                for (int i = 1; i < partiesTable.Rows.Count; ++i)
                {
                    DataRow row = partiesTable.Rows[i];
                    uint id = Convert.ToUInt32(row[0]);
                    uint nomenclatureId = Convert.ToUInt32(row[1]);
                    IPart part = new Part(id, nomenclatureId);

                    if (partiesList.Any((t) => t.Id == part.Id))
                    {
                        throw new ApplicationException("Есть совпадающие идентификаторы партии");
                    }
                    partiesList.Add(part);
                }
                return partiesList;
            }
            catch (InvalidCastException)
            {
                throw new ApplicationException("В одной из строк неверно указана партия");
            }
            catch (FormatException)
            {
                throw new ApplicationException("В одной из строк неверно указана партия");
            }
            catch (OverflowException)
            {
                throw new ApplicationException("В одной из строк неверно указана партия");
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
            if (table.Columns.Count > 2)
            {
                throw new ApplicationException("Слишком много столбцов в таблице");
            }
            if (table.Rows.Count < 2)
            {
                throw new ApplicationException("Нет данных в таблице");
            }
            DataRow firstRow = table.Rows[0];

            if (firstRow[0].ToString() != _idFieldName || firstRow[1].ToString() != _nomenclatureIdFieldName)
            {
                throw new ApplicationException($"Не найдены соответствующие столбцы {_idFieldName} и {_nomenclatureIdFieldName} для партии");
            }
        }
    }
}
