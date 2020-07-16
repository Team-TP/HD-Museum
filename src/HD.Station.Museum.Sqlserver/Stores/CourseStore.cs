using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.SqlServer.Stores
{
    public class CourseStore : StoreBase<Courses, Guid>, ICourseStore
    {
        private MuseumDbContext _dbContext;
        public CourseStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }

    }
}
