using MoreLinq;
using SchedulerMaker.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerMaker.Models
{
    public class Scheduler
    {
        public List<ISchedule> ScheduleList { get; } = new List<ISchedule>();

        private List<int> _timesOfExecution;

        public event Action<string> WarningEvent;

        public List<ISchedule> MakeSchedule(List<IMachineTool> machineTools, List<IOperationTime> operationTimes, List<IPart> parties)
        {
            _timesOfExecution = new List<int>();
            for (int i = 0; i < machineTools.Count; ++i)
            {
                _timesOfExecution.Add(0);
            }

            foreach (var part in parties)
            {
                int partId = part.NomenclatureId;

                IEnumerable<IMachineTool> availableMachineTools = from op in operationTimes
                                                                  from mt in machineTools
                                                                  where op.NomenclatureId == partId && mt.Id == op.MachineToolId
                                                                  select mt;

                IEnumerable<IOperationTime> availableOperations = from op in operationTimes
                                                                  from mt in machineTools
                                                                  where op.NomenclatureId == partId && mt.Id == op.MachineToolId
                                                                  select op;

                if (availableMachineTools.Count() == 0)
                {
                    WarningEvent?.Invoke($"Нет доступного оборудования для обработки материала с идентификатором {part.Id}");
                }

                IOperationTime prefferedOperation = availableOperations.MinBy((ot) => ot.ExecutionTime);

                int indexOfPreferredAvailableMachine = prefferedOperation.MachineToolId;
                int minTimeSpent = _timesOfExecution[indexOfPreferredAvailableMachine];
                foreach (var mt in availableMachineTools)
                {
                    int index = mt.Id;
                    if (_timesOfExecution[index] < minTimeSpent)
                    {
                        minTimeSpent = _timesOfExecution[index];
                        indexOfPreferredAvailableMachine = index;
                    }
                }

                IMachineTool machineTool = availableMachineTools.Single((mt) => mt.Id == indexOfPreferredAvailableMachine);

                IOperationTime operationTime = operationTimes.Find((ot) => ot.NomenclatureId == partId && ot.MachineToolId == machineTool.Id);

                int startTime = _timesOfExecution[indexOfPreferredAvailableMachine];
                _timesOfExecution[indexOfPreferredAvailableMachine] += operationTime.ExecutionTime;
                int endTime = _timesOfExecution[indexOfPreferredAvailableMachine];

                ISchedule schedule = new Schedule(part, machineTool, startTime, endTime);

                ScheduleList.Add(schedule);
            }

            return ScheduleList;
        }
    }
}
