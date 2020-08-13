using HD.Data;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.Stores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
                .Include(a=>a.CategoryMachines)
                .ThenInclude(a=>a.Category)
                .Where(a => a.ParentId == null);
            return d;
        }

        public async Task<Machines> ReadMachineByIdAsync(Guid id)
        {
            var machines = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                    .Include(a => a.MachineWarehouses)
                                                    .Include(a => a.ChildrenMachine)
                                                    .Where(a => a.Id == id)
                                                    .FirstOrDefaultAsync();
            var categoriIds = DbContext.Set<CategoryMachines>().Include(a => a.Category).Where(a => a.MachineId == id).Select(a=>a.CategoryId).ToArray();
            machines.CateIds = categoriIds;
            return machines;
        }

        public async Task<Machines> AddAsync(MachineComponentsViewModel model)
        {
            await DbContext.Database.BeginTransactionAsync(CancellationToken);
            var machineEntry = await DbContext.AddAsync(model.Machine);

            if(model.Machine.CateIds != null)
            {
                foreach (var category in model.Machine.CateIds)
                {
                    await DbContext.AddAsync(new CategoryMachines(machineEntry.Entity.Id, category));
                }
            }           
            if(model.MachineProduce != null)
            {
                model.MachineProduce.MachineId = machineEntry.Entity.Id;
                await DbContext.AddAsync(model.MachineProduce);
            }          
            if(model.MachineWareHouse != null)
            {
                model.MachineWareHouse.MachineId = machineEntry.Entity.Id;
                await DbContext.AddAsync(model.MachineWareHouse);
            }

            await DbContext.SaveChangesAsync(CancellationToken);
            DbContext.Database.CommitTransaction();
            return machineEntry.Entity;
        }

        public override async Task<OperationResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await DbContext.Database.BeginTransactionAsync(CancellationToken);
                var machine = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                        .Include(a => a.MachineWarehouses)
                                                        .Include(a => a.ChildrenMachine)
                                                        .Include(a=>a.CategoryMachines)
                                                        .Where(a => a.Id == id)
                                                        .FirstAsync();
                var machineChild = DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                        .Include(a => a.MachineWarehouses)
                                                        .Where(a => a.ParentId == id);
              

                if(machineChild.Count() > 0)
                {
                    foreach( var child in machineChild)
                    {
                        if(child.MachineProduces != null)
                        {
                            DbContext.Remove(machine.MachineProduces);
                        }
                        if (machine.MachineWarehouses != null)
                        {
                            DbContext.Remove(machine.MachineWarehouses);
                        }
                        DbContext.Remove(child);
                    }
                }
                if (machine.MachineProduces != null)
                {
                    DbContext.Remove(machine.MachineProduces);
                }
                if (machine.MachineWarehouses != null)
                {
                    DbContext.Remove(machine.MachineWarehouses);
                }
                var categories = DbContext.Set<CategoryMachines>().Where(a => a.MachineId == machine.Id);
                if(categories.Count() > 0)
                {
                    foreach (var cate in categories)
                    {
                        DbContext.Remove(cate);
                    }
                }
                DbContext.Remove(machine);
                await DbContext.SaveChangesAsync(CancellationToken);
                DbContext.Database.CommitTransaction();
                return OperationResult.Success;
            }
            catch (Exception ex)
            {
                DbContext.Database.RollbackTransaction();
                return (OperationResult.Failed(ex));
            }
        }
        public override async Task<OperationResult> UpdateAsync(Machines model)
        {
            try
            {
                await DbContext.Database.BeginTransactionAsync(CancellationToken);
                var entry = DbContext.Update(model);
                //if(model.CateIds.Count() > 0)
                //{
                //    if  ((DbContext.Set<CategoryMachines>().Include(a => a.Machine).Where(i => i.MachineId == model.Id)).Count() == 0)
                //    {
                //        foreach (var category in model.CateIds)
                //        {
                //            await DbContext.AddAsync(new CategoryMachines(entry.Entity.Id, category));                     
                //        }
                //    }
                //    else
                //    {
                //        DbContext.Update(model.CategoryMachines);
                //    }
                //}

                if (model.MachineProduces != null)
                {
                    if ((DbContext.Set<MachineProduces>().Include(a => a.Machines).Where(i => i.MachineId == model.Id)).Count() == 0)
                    {
                        model.MachineProduces.MachineId = entry.Entity.Id;
                        await DbContext.AddAsync(model.MachineProduces);
                    }
                    else
                    {
                        DbContext.Update(model.MachineProduces);
                    }
                }
                if (model.MachineWarehouses != null)
                {
                    if ((DbContext.Set<MachineWareHouses>().Include(a => a.Machines).Where(i => i.MachineId == model.Id)).Count() == 0)
                    {
                        model.MachineWarehouses.MachineId = entry.Entity.Id;
                        await DbContext.AddAsync(model.MachineWarehouses);
                    }
                    else
                    {
                        DbContext.Update(model.MachineWarehouses);
                    }
                }
                await DbContext.SaveChangesAsync(CancellationToken);
                DbContext.Database.CommitTransaction();
                return (OperationResult.Success);
            }
            catch (Exception ex)
            {
                DbContext.Database.RollbackTransaction();
                return (OperationResult.Failed(ex));
            }
        }

    }
}
