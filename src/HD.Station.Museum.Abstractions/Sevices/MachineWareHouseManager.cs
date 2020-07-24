using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class MachineWareHouseManager : ManagerBase<MachineWareHouses, Guid>, IMachineWareHouseManager
    {
        public MachineWareHouseManager(IServiceProvider serviceProvider, IMachineWareHouseStore store) : base(serviceProvider, store)
        {
        }
    }
}
