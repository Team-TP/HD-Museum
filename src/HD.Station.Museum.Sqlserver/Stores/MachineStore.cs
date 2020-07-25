using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Museum.Sqlserver.Stores
{
    public class MachineStore : StoreBase<Machines, Guid>, IMachineStore
    {
        private MuseumDbContext _dbContext;
        public MachineStore(MuseumDbContext dbContext, IServiceProvider serviceProvider) : base(dbContext, serviceProvider)
        {
            _dbContext = dbContext;
        }

        public override IQueryable<Machines> Query()
        {
            var d = base.Query()
                .Include(a => a.MachineProduces)
                .Include(a => a.MachineWarehouses);
            return d;
        }

        public override async Task<Machines> ReadByIdAsync(Guid id)
        {
            var d = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                    .Include(a => a.MachineWarehouses)
                                                    .Include(a=>a.ChildrenMachine)
                                                    .Where(a => a.Id == id)
                                                    .FirstOrDefaultAsync();
            return d;
        }

    }
}
