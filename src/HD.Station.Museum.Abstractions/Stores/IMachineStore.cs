using HD.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HD.Station.Museum.Stores
{
    public interface IMachineStore : IStore<Machines, Guid>
    {
        Task<Machines> ReadMachineByIdAsync(Guid id);
    }
}
