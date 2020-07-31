using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Museum
{
    public interface IMachineManager : IManager<Machines, Guid>
    {
        Task<(OperationResult State, Machines ViewItem)> ReadMachineByIdAsync(Guid id, OperationAuthorizationRequirement requirement = null);

        Task<(OperationResult State, Machines Edit)> AddAsync(MachineComponentsViewModel model);
    }
}
