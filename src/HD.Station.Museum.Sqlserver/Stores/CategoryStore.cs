using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HD.Station.Museum.SqlServer.Stores
{
    public class CategoryStore : StoreBase<Categories, Guid>, ICategoryStore
    {
        private MuseumDbContext _dbContext;
        public CategoryStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }

        public override IQueryable<Categories> Query()
        {
            return _dbContext.Set<Categories>().Include(a => a.ChildrenCategory);
        }

    }
}
