using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class MachineCreateViewModel
    {
        public MachinesEditViewModel Machine { get; set; }
        public MachineProduceEditViewModel MachineProduce { get; set; }
        public MachineWareHouseEditViewModel MachineWareHouse { get; set; }


        //public Machines ToModel()
        //{
        //    return new Machines
        //    {
        //        Id = Machine.Id,
        //        Name = Machine.Name,
        //        Description = Machine.Description,
        //        Disabled = Machine.Disabled,
        //        ParentId = Machine.ParentId,
        //        Stage = Machine.Stage,
        //        Amount = Machine.Amount,
        //        Price = Machine.Price,
        //        MachineProduces = MachineProduce?.ToModel(),
        //        MachineWarehouses = MachineWareHouse?.ToModel()
        //    };
        //}


    }

}
