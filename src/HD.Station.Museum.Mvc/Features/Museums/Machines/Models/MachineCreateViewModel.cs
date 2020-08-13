using HD.Station.Museum;
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



        //public MachineCreateViewModel(Machines model)
        //{
        //    if (model != null)
        //    {
        //        Machine = new MachinesEditViewModel(model);
        //        //Machine.Id = model.Id;
        //        //Machine.Name = model.Name;
        //        //Machine.Description = model.Description;
        //        //Machine.Disabled = model.Disabled;
        //        //Machine.Amount = model.Amount;
        //        //Machine.Stage = model.Stage;
        //        //Machine.Price = model.Price;
        //        if (model.MachineProduces != null) { MachineProduce = new MachineProduceEditViewModel(model.MachineProduces); }
        //        if (model.MachineWarehouses != null) { MachineWareHouse = new MachineWareHouseEditViewModel(model.MachineWarehouses); }
        //    }
        //}

        public Machines ToModel()
        {
            return new Machines
            {
                Id = Machine.Id,
                Name = Machine.Name,
                Description = Machine.Description,
                Disabled = Machine.Disabled,
                ParentId = Machine.ParentId,
                Stage = Machine.Stage,
                Amount = Machine.Amount,
                Price = Machine.Price,
                CateIds = Machine.CateIds,
                MachineProduces = MachineProduce?.ToModel(),
                MachineWarehouses = MachineWareHouse?.ToModel(),
            };
        }


    }

}
