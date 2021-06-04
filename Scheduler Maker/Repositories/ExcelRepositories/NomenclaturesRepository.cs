using ExcelDataReader;
using ExcelDataReader.Exceptions;
using SchedulerMaker.Models;
using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    public class NomenclaturesRepository : IDataRepository<INomenclature>
    {
        private readonly string _idFieldName = "id";
        private readonly string _nomenclatureFieldName = "nomenclature";
        private readonly DataSet _nomenclatures = null;

        public NomenclaturesRepository(string path)
        {
            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var result = reader.AsDataSet();
                        _nomenclatures = result;
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
        public IEnumerable<INomenclature> GetDataList()
        {
            try
            {
                DataSet nomenclaturesDataSet = _nomenclatures;
                ValidateDataSet(nomenclaturesDataSet);
                DataTable nomenclaturesTable = nomenclaturesDataSet.Tables[0];
                List<INomenclature> nomenclaturesList = new List<INomenclature>();
                for (int i = 1; i < nomenclaturesTable.Rows.Count; ++i)
                {
                    DataRow row = nomenclaturesTable.Rows[i];
                    int id = Convert.ToInt32(row[0]);
                    string nomenclatureName = Convert.ToString(row[1]);
                    INomenclature nomenclature = new Nomenclature(id, nomenclatureName);

                    if (nomenclaturesList.Any((t) => t.Id == nomenclature.Id))
                    {
                        throw new ApplicationException("Есть совпадающие идентификаторы номенклатуры");
                    }
                    nomenclaturesList.Add(nomenclature);
                }
                return nomenclaturesList;
            }
            catch (InvalidCastException)
            {
                throw new ApplicationException("В одной из строк неверно указаны данные о номенклатуре");
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

            if (firstRow[0].ToString() != _idFieldName || firstRow[1].ToString() != _nomenclatureFieldName)
            {
                throw new ApplicationException($"Не найдены соответствующие столбцы {_idFieldName} и {_nomenclatureFieldName} для партии");
            }
        }

    }
}
