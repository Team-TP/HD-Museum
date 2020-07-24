using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sqlserver.Stores
{
    public class CategoryMachineStore : StoreBase<CategoryMachines, Guid>, ICategoryMachineStore
    {
        private MuseumDbContext _dbContext;
        public CategoryMachineStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }
    }
}
