﻿using HD.Data;
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
                .Where(a => a.ParentId == null);
            return d;
        }

        public async Task<Machines> ReadMachineByIdAsync(Guid id)
        {
            var d = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                    .Include(a => a.MachineWarehouses)
                                                    .Include(a => a.ChildrenMachine)
                                                    .Where(a => a.Id == id)
                                                    .FirstOrDefaultAsync();


            return d;
        }

        //public async Task<Machines> AddAsync(MachineComponentsViewModel model)
        //{
        //    await DbContext.Database.BeginTransactionAsync(CancellationToken);
        //    var machineEntry = await DbContext.AddAsync(model.Machine);

        //    model.MachineProduce.MachineId = machineEntry.Entity.Id;
        //    await DbContext.AddAsync(model.MachineProduce);

        //    model.MachineWareHouse.MachineId = machineEntry.Entity.Id;
        //    await DbContext.AddAsync(model.MachineWareHouse);

        //    await DbContext.SaveChangesAsync(CancellationToken);
        //    DbContext.Database.CommitTransaction();
        //    return machineEntry.Entity;
        //}

        public override async Task<OperationResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                await DbContext.Database.BeginTransactionAsync(CancellationToken);
                var machine = await DbContext.Set<Machines>().Include(a => a.MachineProduces)
                                                        .Include(a => a.MachineWarehouses)
                                                        .Include(a => a.ChildrenMachine)
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
