using System.Collections.Generic;

namespace SchedulerMaker.Models.Interfaces
{
    public interface IDataRepository<T>
        where T : class
    {
        IEnumerable<T> GetDataList();
    }
}
