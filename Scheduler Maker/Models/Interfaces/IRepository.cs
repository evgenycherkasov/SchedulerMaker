using System.Collections.Generic;

namespace SchedulerMaker.Models.Interfaces
{
    interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetDataList();
    }
}
