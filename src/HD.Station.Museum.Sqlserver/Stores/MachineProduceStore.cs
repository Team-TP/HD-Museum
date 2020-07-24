using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sqlserver.Stores
{
    public class MachineProduceStore : StoreBase<MachineProduces, Guid>, IMachineProduceStore
    {
        private MuseumDbContext _dbContext;
        public MachineProduceStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }
    }
}
