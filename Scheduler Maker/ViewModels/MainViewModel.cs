using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SchedulerMaker.Models.Contexts.Excel;
using SchedulerMaker.Models.Interfaces;
using SchedulerMaker.Repositories.ExcelRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerMaker.ViewModels
{
    class MainViewModel : BindableBase
    {
        #region Contexts

        public ExcelPartiesContext ExcelParties { get; set; }
        public ExcelMachineToolsContext ExcelMachineTools { get; set; }
        public ExcelNomenclaturesContext ExcelNomenclatures { get; set; }

        #endregion

        public ObservableCollection<IPart> Parties { get; set; } = new ObservableCollection<IPart>();
        public ObservableCollection<IMachineTool> MachineTools { get; set; } = new ObservableCollection<IMachineTool>();
        public ObservableCollection<INomenclature> Nomenclatures { get; set; } = new ObservableCollection<INomenclature>();
        public ObservableCollection<IOperationTime> OperationTimes { get; set; } = new ObservableCollection<IOperationTime>();
        public ObservableCollection<ISchedule> Schedule { get; set; } = new ObservableCollection<ISchedule>();

        public DelegateCommand ReadPartiesCommand { get; private set; }
        public DelegateCommand ReadMachineToolsCommand { get; private set; }
        public DelegateCommand ReadNomenclaturesCommand { get; private set; }
        public DelegateCommand ReadOperationTimesCommand { get; private set; }
        public DelegateCommand UploadAllData { get; private set; }

        public MainViewModel()
        {
            UploadAllData = new DelegateCommand(() =>
            {
                
            });

            ReadPartiesCommand = new DelegateCommand(() =>
            {
                Parties.Clear();

                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Excel|*.xls;*.xlsx",
                    Title = "Откройте файл с партиями"
                };

                if (ofd.ShowDialog() != true)
                {
                    return;
                }

                string filePath = ofd.FileName;
                ExcelParties = new ExcelPartiesContext(filePath);
                var parties = ExcelParties.GetDataList();
                Parties.AddRange(parties);
            });

            ReadMachineToolsCommand = new DelegateCommand(() =>
            {
                MachineTools.Clear();

                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Excel|*.xls;*.xlsx",
                    Title = "Откройте файл с оборудованием"
                };

                if (ofd.ShowDialog() != true)
                {
                    return;
                }

                string filePath = ofd.FileName;
                ExcelMachineTools = new ExcelMachineToolsContext(filePath);
                var parties = ExcelMachineTools.GetDataList();
                MachineTools.AddRange(parties);
            });

            ReadNomenclaturesCommand = new DelegateCommand(() =>
            {
                Nomenclatures.Clear();

                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Excel|*.xls;*.xlsx",
                    Title = "Откройте файл с номенклатурой"
                };

                if (ofd.ShowDialog() != true)
                {
                    return;
                }

                string filePath = ofd.FileName;
                ExcelNomenclatures = new ExcelNomenclaturesContext(filePath);
                var parties = ExcelNomenclatures.GetDataList();
                Nomenclatures.AddRange(parties);
            });

            ReadOperationTimesCommand = new DelegateCommand(() =>
            {
                OperationTimes.Clear();

                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Excel|*.xls;*.xlsx",
                    Title = "Откройте файл с временами выполнения"
                };

                if (ofd.ShowDialog() != true)
                {
                    return;
                }

                string filePath = ofd.FileName;
                ExcelMachineTools = new ExcelMachineToolsContext(filePath);
                var parties = ExcelMachineTools.GetDataList();
                //OperationTimes.AddRange(parties);
            });
        }
    }
}
