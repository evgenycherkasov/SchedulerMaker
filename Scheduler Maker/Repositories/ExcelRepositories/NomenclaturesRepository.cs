using SchedulerMaker.Models;
using SchedulerMaker.Models.Contexts.Excel;
using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class NomenclaturesRepository : IRepository<INomenclature>
    {
        private ExcelNomenclaturesContext _excelContext = null;

        private readonly string _idFieldName = "id";
        private readonly string _nomenclatureFieldName = "nomenclature";

        public NomenclaturesRepository(ExcelNomenclaturesContext excelNomenclaturesContext)
        {
            _excelContext = excelNomenclaturesContext;
        }
        public IEnumerable<INomenclature> GetDataList()
        {
            //TODO: обработать IOException
            DataSet nomenclaturesDataSet = _excelContext.NomenclaturesDataSet;
            ValidateDataSet(nomenclaturesDataSet);
            DataTable nomenclaturesTable = nomenclaturesDataSet.Tables[0];
            List<INomenclature> nomenclaturesList = new List<INomenclature>();
            for (int i = 1; i < nomenclaturesTable.Rows.Count; ++i)
            {
                DataRow row = nomenclaturesTable.Rows[i];
                int id = Convert.ToInt32(row[0]);
                string nomenclatureName = Convert.ToString(row[1]);
                NomenclatureClass nomenclature = new NomenclatureClass(id, nomenclatureName);

                if (nomenclaturesList.Any((t) => t.Id == nomenclature.Id))
                {
                    throw new ApplicationException("Есть совпадающие идентификаторы номенклатуры");
                }
                nomenclaturesList.Add(nomenclature);
            }
            return nomenclaturesList;
        }

        public void WriteDataList(IEnumerable<INomenclature> data, string path)
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
