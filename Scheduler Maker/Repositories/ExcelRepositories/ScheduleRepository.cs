using SchedulerMaker.Models.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.Repositories.ExcelRepositories
{
    class ScheduleRepository : IScheduleRepository<ISchedule>
    {
        private readonly string _mcToolId = "ID оборудования";
        private readonly string _mcToolName = "Наименование оборудования";
        private readonly string _partId = "ID партии";
        private readonly string _nomenclatureId = "ID номенклатуры";
        private readonly string _startTime = "Время начала";
        private readonly string _endTime = "Время окончания";

        private readonly string _path;

        private Dictionary<string, string> _headerCellValuePairs = new Dictionary<string, string>();

        public ScheduleRepository(string path)
        {
            _path = path;

            _headerCellValuePairs.Add("A1", _mcToolId);
            _headerCellValuePairs.Add("B1", _mcToolName);
            _headerCellValuePairs.Add("C1", _partId);
            _headerCellValuePairs.Add("D1", _nomenclatureId);
            _headerCellValuePairs.Add("E1", _startTime);
            _headerCellValuePairs.Add("F1", _endTime);
        }

        public void UnloadData(IEnumerable<ISchedule> schedule)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("Расписание");

                foreach(var header in _headerCellValuePairs)
                {
                    worksheet.Cell(header.Key).Value = header.Value;
                }

                int index = 0;
                foreach (var sch in schedule)
                {
                    worksheet.Cell(2 + index, 1).Value = sch.MachineTool.Id;
                    worksheet.Cell(2 + index, 2).Value = sch.MachineTool.Name;
                    worksheet.Cell(2 + index, 3).Value = sch.Part.Id;
                    worksheet.Cell(2 + index, 4).Value = sch.Part.NomenclatureId;
                    worksheet.Cell(2 + index, 5).Value = sch.StartTime;
                    worksheet.Cell(2 + index, 6).Value = sch.EndTime;
                    index++;
                }
                workbook.SaveAs(_path);
            }
        }
    }
}
