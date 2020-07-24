using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class MachineProduceManager : ManagerBase<MachineProduces, Guid>, IMachineProduceManager
    {
        public MachineProduceManager(IServiceProvider serviceProvider, IMachineProduceStore store) : base(serviceProvider, store)
        {
        }
    }
}
