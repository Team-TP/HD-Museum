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
                .Include(a => a.MachineWarehouses)
                .Where(a => a.ParentId == null);
            return d;
        }

        public async Task<Machines> ReadMachineByIdAsync(Guid id)
        {
            var d = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                    .Include(a => a.MachineWarehouses)
                                                    .Include(a=>a.ChildrenMachine)
                                                    .Where(a => a.Id == id)
                                                    .FirstOrDefaultAsync();
            return d;
        }

        //public override async Task<OperationResult> UpdateAsync(Machines model)
        //{
        //    var entry = DbContext.Update(model);
        //    if (model.MachineProduces != null)
        //    {
        //        if ((DbContext.Set<MachineProduces>().Include(a => a.Machines).Where(i => i.MachineId == model.Id)).Count() == 0)
        //        {
        //            model.MachineProduces.MachineId = entry.Entity.Id;
        //            await DbContext.AddAsync(model.MachineProduces);
        //        }
        //        else
        //        {
        //            DbContext.Update(model.MachineProduces);
        //        }
        //    }
        //    if (model.MachineWarehouses != null)
        //    {
        //        if ((DbContext.Set<MachineWareHouses>().Include(a => a.Machines).Where(i => i.MachineId == model.Id)).Count() == 0)
        //        {
        //            model.MachineWarehouses.MachineId = entry.Entity.Id;
        //            await DbContext.AddAsync(model.MachineWarehouses);
        //        }
        //        else
        //        {
        //            DbContext.Update(model.MachineWarehouses);
        //        }
        //    }
        //    await DbContext.SaveChangesAsync(CancellationToken);
        //    DbContext.Database.CommitTransaction();
        //    return (OperationResult.Success);
        //}

    }
}
