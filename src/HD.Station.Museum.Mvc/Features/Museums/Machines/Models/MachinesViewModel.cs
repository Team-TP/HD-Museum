using HD.Station.ComponentModel;
using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class MachinesViewModel : ViewBase<Machines, Guid>
    {
        public MachinesViewModel() { }
        public MachinesViewModel(Machines machines)
        {
            if (machines != null)
            {
                Id = machines.Id;
                Name = machines.Name;
                Description = machines.Description;
                Disabled = machines.Disabled;
                ParentId = machines.ParentId;
                Stage = machines.Stage;
                Amount = machines.Amount;
                Price = machines.Price;
                CateIds = machines.CateIds;
                ChildMachines = machines.ChildrenMachine;
                MachineProduce = new MachineProduceViewModel(machines.MachineProduces);
                MachineWareHouse = new MachineWareHouseViewModel(machines.MachineWarehouses);

                //if (machines.MachineProduces != null) { MachineProduce = new MachineProduceEditViewModel(machines.MachineProduces); }
                //if (machines.MachineWarehouses != null) { MachineWareHouse = MachineProduce }

            }
        }
        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public string Name { get; set; }
        [Display]
        [GridDisplay]
        public string Description { get; set; }

        public bool Disabled { get; set; }
        [Hidden]
        public Guid? ParentId { get; set; }

        [Display]
        [GridDisplay]
        public StageType Stage { get; set; }

        [Display]
        [GridDisplay]
        public int Amount { get; set; }

        [Display]
        [GridDisplay]
        public decimal Price { get; set; }

        
        [Display(Name = "Date Of Manufacture")]
        [GridDisplay(Title = "Date Of Manufacture")]
        public string DateOfManufactureString => MachineProduce?.DateOfManufacture?.ToString("HH:mm dd/MM/yyyy");
        //public DateTimeOffset DateOfManufacture => MachineProduce.DateOfManufacture;
        [Display]
        [GridDisplay]
        public string Address => MachineWareHouse.Address;
        [Display]
        [GridDisplay]
        public string Phone => MachineWareHouse.Phone;
        public IEnumerable<Guid> CateIds { get; set; }


        //public MachineProduces MachineProduce { get; set; }
        //public MachineWareHouses MachineWareHouse { get; set; }

        public MachineProduceViewModel MachineProduce { get; set; }

        public MachineWareHouseViewModel MachineWareHouse { get; set; }
        public IEnumerable<Machines> ChildMachines { get; set; }
        public override Machines ToModel()
        {
            var machines = new Machines
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Disabled = Disabled,
                ParentId = ParentId,
                Stage = Stage,
                Amount = Amount,
                Price = Price,
                CateIds = CateIds,

        };
            return machines;
        }
    }
}
