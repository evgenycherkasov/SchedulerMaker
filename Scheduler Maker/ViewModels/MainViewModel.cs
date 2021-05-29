using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SchedulerMaker.Models.Interfaces;
using SchedulerMaker.Repositories.ExcelRepositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchedulerMaker.ViewModels
{
    class MainViewModel : BindableBase
    {
        #region Repositories

        public MachineToolsRepository MachineToolsRepo { get; set; } = null;
        public PartRepository PartiesRepo { get; set; } = null;
        public NomenclaturesRepository NomenclaturesRepo { get; set; } = null;
        public OperationTimesRepository OperationTimesRepo { get; set; } = null;

        #endregion

        public ObservableCollection<IPart> Parties { get; } = new ObservableCollection<IPart>();
        public ObservableCollection<IMachineTool> MachineTools { get; } = new ObservableCollection<IMachineTool>();
        public ObservableCollection<INomenclature> Nomenclatures { get; } = new ObservableCollection<INomenclature>();
        public ObservableCollection<IOperationTime> OperationTimes { get; } = new ObservableCollection<IOperationTime>();
        public ObservableCollection<ISchedule> Schedule { get; } = new ObservableCollection<ISchedule>();

        public DelegateCommand ReadPartiesCommand { get; private set; }
        public DelegateCommand ReadMachineToolsCommand { get; private set; }
        public DelegateCommand ReadNomenclaturesCommand { get; private set; }
        public DelegateCommand ReadOperationTimesCommand { get; private set; }
        public DelegateCommand UploadAllData { get; private set; }

        public MainViewModel()
        {
            UploadAllData = new DelegateCommand(() =>
            {
                try
                {
                    ReadPartiesCommand.Execute();
                    ReadMachineToolsCommand.Execute();
                    ReadNomenclaturesCommand.Execute();
                    ReadOperationTimesCommand.Execute();
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            ReadPartiesCommand = new DelegateCommand(() =>
            {
                try
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
                    PartiesRepo = new PartRepository(filePath);
                    var parties = PartiesRepo.GetDataList();
                    Parties.AddRange(parties);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            ReadMachineToolsCommand = new DelegateCommand(() =>
            {
                try
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

                    MachineToolsRepo = new MachineToolsRepository(filePath);
                    var machineTools = MachineToolsRepo.GetDataList();
                    MachineTools.AddRange(machineTools);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            ReadNomenclaturesCommand = new DelegateCommand(() =>
            {
                try
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
                    NomenclaturesRepo = new NomenclaturesRepository(filePath);
                    var nomenclatures = NomenclaturesRepo.GetDataList();
                    Nomenclatures.AddRange(nomenclatures);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });

            ReadOperationTimesCommand = new DelegateCommand(() =>
            {
                try
                {
                    OperationTimes.Clear();

                    OpenFileDialog ofd = new OpenFileDialog
                    {
                        Filter = "Excel|*.xls;*.xlsx",
                        Title = "Откройте файл с временами обработки партий"
                    };

                    if (ofd.ShowDialog() != true)
                    {
                        return;
                    }

                    string filePath = ofd.FileName;
                    OperationTimesRepo = new OperationTimesRepository(filePath);
                    var operationTimes = OperationTimesRepo.GetDataList();
                    OperationTimes.AddRange(operationTimes);
                }
                catch (ApplicationException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            });
        }
    }
}
