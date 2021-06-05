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

        private List<uint> _timesOfExecution;

        public event Action<string> WarningEvent;

        public List<ISchedule> MakeSchedule(List<IMachineTool> machineTools, List<IOperationTime> operationTimes, List<IPart> parties)
        {
            _timesOfExecution = new List<uint>();
            for (int i = 0; i < machineTools.Count; ++i)
            {
                _timesOfExecution.Add(0);
            }

            foreach (var part in parties)
            {
                uint partId = part.NomenclatureId;

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
                    continue;
                }

                IOperationTime prefferedOperation = availableOperations.MinBy((ot) => ot.ExecutionTime);

                int indexOfPreferredAvailableMachine = (int)prefferedOperation.MachineToolId;
                uint minTimeSpent = _timesOfExecution[indexOfPreferredAvailableMachine];
                foreach (var mt in availableMachineTools)
                {
                    int index = (int)mt.Id;
                    if (_timesOfExecution[index] < minTimeSpent)
                    {
                        minTimeSpent = _timesOfExecution[index];
                        indexOfPreferredAvailableMachine = index;
                    }
                }

                IMachineTool machineTool = availableMachineTools.Single((mt) => mt.Id == indexOfPreferredAvailableMachine);

                IOperationTime operationTime = operationTimes.Find((ot) => ot.NomenclatureId == partId && ot.MachineToolId == machineTool.Id);

                uint startTime = _timesOfExecution[indexOfPreferredAvailableMachine];
                _timesOfExecution[indexOfPreferredAvailableMachine] += operationTime.ExecutionTime;
                uint endTime = _timesOfExecution[indexOfPreferredAvailableMachine];

                ISchedule schedule = new Schedule(part, machineTool, startTime, endTime);

                ScheduleList.Add(schedule);
            }

            return ScheduleList;
        }
    }
}
