using HD.Station.Museum.Stores;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
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
    }
}
