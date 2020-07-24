using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class MachineManager : ManagerBase<Machines, Guid>, IMachineManager
    {
        public MachineManager(IServiceProvider serviceProvider, IMachineStore store) : base(serviceProvider, store)
        {
        }
    }
}
