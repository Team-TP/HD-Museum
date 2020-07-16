using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Museum.SqlServer.Stores
{
    public class EnrollmentStore : StoreBase<Enrollments, Guid>, IEnrollmentStore
    {
        MuseumDbContext _dbContext;
        public EnrollmentStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }
        public override IQueryable<Enrollments> Query()
        {
            var d = base.Query()
                .Include(a => a.Courses)
                .Include(a => a.Student); 
            return d;
        }
        public override async Task<Enrollments> ReadByIdAsync(Guid id)
        {
            var d = await DbContext.Set<Enrollments>().Include(a => a.Courses)
                .Include(a => a.Student)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            return d;
        }


    }
}
