using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class StudentManager : ManagerBase<Students, Guid>, IStudentManager
    {
        public StudentManager(IServiceProvider serviceProvider, IStudentStore store) : base(serviceProvider, store)
        {
        }
    }
}
