using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using SchedulerMaker.Models;
using SchedulerMaker.Models.Interfaces;
using SchedulerMaker.Repositories.ExcelRepositories;
using SchedulerMaker.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SchedulerMaker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Pages

        public MainMenuPage MainMenuPage { get; } = null;
        public PartiesPage PartiesPage { get; } = null;
        public NomenclaturesPage NomenclaturesPage { get; } = null;
        public MachineToolsPage MachineToolsPage { get; } = null;
        public OperationTimesPage OperationTimesPage { get; } = null;
        public SchedulePage SchedulePage { get; } = null;

        #endregion

        #region Repositories

        public IDataRepository<IMachineTool> MachineToolsRepo { get; private set; } = null;
        public IDataRepository<IPart> PartiesRepo { get; private set; } = null;
        public IDataRepository<INomenclature> NomenclaturesRepo { get; private set; } = null;
        public IDataRepository<IOperationTime> OperationTimesRepo { get; private set; } = null;
        public IScheduleRepository<ISchedule> ScheduleRepository { get; private set; } = null;

        #endregion

        public ObservableCollection<IPart> Parties { get; } = new ObservableCollection<IPart>();
        public ObservableCollection<IMachineTool> MachineTools { get; } = new ObservableCollection<IMachineTool>();
        public ObservableCollection<INomenclature> Nomenclatures { get; } = new ObservableCollection<INomenclature>();
        public ObservableCollection<IOperationTime> OperationTimes { get; } = new ObservableCollection<IOperationTime>();
        public ObservableCollection<ISchedule> Schedule { get; } = new ObservableCollection<ISchedule>();

        public DelegateCommand ReadPartiesCommand { get; }
        public DelegateCommand ReadMachineToolsCommand { get; }
        public DelegateCommand ReadNomenclaturesCommand { get; }
        public DelegateCommand ReadOperationTimesCommand { get; }
        public DelegateCommand UploadAllDataCommand { get; }
        public DelegateCommand MakeScheduleCommand { get; }
        public DelegateCommand UnloadAllDataCommand { get; }

        public MainViewModel()
        {
            MainMenuPage = new MainMenuPage(this);
            PartiesPage = new PartiesPage(this);
            NomenclaturesPage = new NomenclaturesPage(this);
            MachineToolsPage = new MachineToolsPage(this);
            OperationTimesPage = new OperationTimesPage(this);
            SchedulePage = new SchedulePage(this);

            UploadAllDataCommand = new DelegateCommand(UploadAllDataCommandHandler);

            MakeScheduleCommand = new DelegateCommand(MakeScheduleCommandHandler);

            UnloadAllDataCommand = new DelegateCommand(UnloadAllDataCommandHandler);

            ReadPartiesCommand = new DelegateCommand(ReadPartiesCommandHandler);

            ReadMachineToolsCommand = new DelegateCommand(ReadMachineToolsCommandHandler);

            ReadNomenclaturesCommand = new DelegateCommand(ReadNomenclaturesCommandHandler);

            ReadOperationTimesCommand = new DelegateCommand(ReadOperationTimesCommandHandler);
        }

        private void UploadAllDataCommandHandler()
        {
            try
            {
                ReadPartiesCommandHandler();
                ReadMachineToolsCommandHandler();
                ReadNomenclaturesCommandHandler();
                ReadOperationTimesCommandHandler();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MakeScheduleCommandHandler()
        {
            try
            {
                Schedule.Clear();
                List<IMachineTool> machineTools = MachineTools.ToList();
                List<IPart> parties = Parties.ToList();
                List<IOperationTime> operationTimes = OperationTimes.ToList();

                if (machineTools.Count == 0)
                {
                    throw new ApplicationException("Нет доступного оборудования");
                }
                if (parties.Count == 0)
                {
                    throw new ApplicationException("Нет партий для обработки");
                }
                if (operationTimes.Count == 0)
                {
                    throw new ApplicationException("Нет параметров, задающих обработку");
                }

                Scheduler scheduler = new Scheduler();
                scheduler.WarningEvent += ShowWarningHandler;
                List<ISchedule> schedule = scheduler.MakeSchedule(machineTools, operationTimes, parties);
                scheduler.WarningEvent -= ShowWarningHandler;
                Schedule.AddRange(schedule);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UnloadAllDataCommandHandler()
        {
            IEnumerable<ISchedule> schedule = Schedule.ToList();

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Excel|*.xls;*.xlsx",
                Title = "Откройте файл Excel фалй для сохранения"
            };

            if (ofd.ShowDialog() != true)
            {
                return;
            }

            string filePath = ofd.FileName;

            ScheduleRepository = new ScheduleRepository(filePath);

            ScheduleRepository.UnloadData(schedule);
        }

        private void ReadPartiesCommandHandler()
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
        }

        private void ReadMachineToolsCommandHandler()
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
        }

        private void ReadNomenclaturesCommandHandler()
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
        }

        private void ReadOperationTimesCommandHandler()
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
        }

        private void ShowWarningHandler(string message)
        {
            MessageBox.Show(message);
        }
    }
}
