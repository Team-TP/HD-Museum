using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HD.Station.Museum
{
    public class MachineComponentsViewModel
    {
        public Machines Machine { get; set; }
        public MachineProduces MachineProduce { get; set; }
        public MachineWareHouses MachineWareHouse { get; set; }

        //public MachineComponentsViewModel ToModel()
        //{
        //    return new MachineComponentsViewModel
        //    {
        //        Machine = Machine,
        //        MachineProduce = MachineProduce,
        //        MachineWareHouse = MachineWareHouse,
        //    };

        //}

    }
}
