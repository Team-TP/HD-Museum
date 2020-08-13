using HD.Station.Museum.Stores;
using HD.Station.Security;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Museum.Sevices
{
    public class MachineManager : ManagerBase<Machines, Guid>, IMachineManager
    {
        private  IMachineStore _store;
        public MachineManager(IServiceProvider serviceProvider, IMachineStore store) : base(serviceProvider, store)
        {
            _store = store;
        }
        public virtual async Task<(OperationResult State, Machines ViewItem)> ReadMachineByIdAsync(Guid id, OperationAuthorizationRequirement requirement = null)
        {
            var model = await _store.ReadMachineByIdAsync(id);
            if (model == null)
            {
                return (OperationResult.Success, default(Machines));
            }
            var authorizeResult = await AuthorizeAsync(model, requirement);
            return (authorizeResult, model);
        }
        public async Task<(OperationResult State, Machines Edit)> AddAsync(MachineComponentsViewModel model)
        {
            try
            {
                var authResult = await AuthorizeAsync(model.Machine, Operations.Create);
                if (!authResult.Succeeded)
                {
                    return (authResult, null);
                }
                return (OperationResult.Success, await _store.AddAsync(model));
            }
            catch (Exception ex)
            {
                return (OperationResult.Failed(ex), null);
            }
        }
        public override async Task<OperationResult> UpdateAsync(Machines model)
        {
            try
            {
                var authResult = await AuthorizeAsync(model, Operations.Update);
                if (!authResult.Succeeded)
                {
                    return authResult;
                }
                return await _store.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                return (OperationResult.Failed(ex));
            }

        }
        public override async Task<OperationResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var machine = await _store.GetByIdAsync(id);
                var authResult = await AuthorizeAsync(machine, Operations.Delete);
                if (!authResult.Succeeded)
                {
                    return authResult;
                }
                return await _store.DeleteByIdAsync(id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Failed(ex));
            }
        }

        //public virtual Task<IQueryable<Machines>> QueryAsync(string filterText = null, bool includeDisabled = false)
        //{
        //    var data = 
        //}
    }
}
