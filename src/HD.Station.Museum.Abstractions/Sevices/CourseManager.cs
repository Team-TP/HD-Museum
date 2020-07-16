using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class CourseManager : ManagerBase<Courses, Guid>, ICourseManager
    {
        public CourseManager(IServiceProvider serviceProvider, ICourseStore store) : base(serviceProvider, store)
        {
        }
    }
}
