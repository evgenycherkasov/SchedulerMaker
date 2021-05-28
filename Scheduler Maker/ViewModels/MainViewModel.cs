using Prism.Commands;
using Prism.Mvvm;
using SchedulerMaker.Models.Interfaces;
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
        public ObservableCollection<IPart> Parties = new ObservableCollection<IPart>();
        public ObservableCollection<IMachineTool> MachineTools = new ObservableCollection<IMachineTool>();
        public ObservableCollection<INomenclature> Nomenclatures = new ObservableCollection<INomenclature>();
        public ObservableCollection<IOperationTime> OperationTimes = new ObservableCollection<IOperationTime>();
        public ObservableCollection<ISchedule> Schedule = new ObservableCollection<ISchedule>();

        public DelegateCommand ReadPartiesCommand { get; private set; }
        public DelegateCommand ReadMachineToolsCommand { get; private set; }
        public DelegateCommand ReadNomenclaturesCommand { get; private set; }
        public DelegateCommand ReadOperationTimesCommand { get; private set; }

        public MainViewModel()
        {
            ReadPartiesCommand = new DelegateCommand(() =>
            {

            });

            ReadMachineToolsCommand = new DelegateCommand(() =>
            {

            });

            ReadNomenclaturesCommand = new DelegateCommand(() =>
            {

            });

            ReadOperationTimesCommand = new DelegateCommand(() =>
            {

            });
        }
    }
}
