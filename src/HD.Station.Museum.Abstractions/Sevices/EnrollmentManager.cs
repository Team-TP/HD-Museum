using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class EnrollmentManager : ManagerBase<Enrollments, Guid>, IEnrollmentManager
    {
        public EnrollmentManager(IServiceProvider serviceProvider, IEnrollmentStore store) : base(serviceProvider, store)
        {

        }
    }
}
