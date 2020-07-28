using HD.Station.ComponentModel;
using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class MachinesEditViewModel : ViewBase<Machines, Guid>
    {
        public MachinesEditViewModel() { }
        public MachinesEditViewModel(Machines machines)
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
                MachineProduces = new MachineProduceEditViewModel(machines.MachineProduces);
                MachineWarehouses = new MachineWareHouseEditViewModel(machines.MachineWarehouses);

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

        public Guid? ParentId { get; set; }
        public MachineType Stage { get; set; }
        [Display]
        [GridDisplay]
        public int Amount { get; set; }
        [Display]
        [GridDisplay]
        public decimal Price { get; set; }

        //public DateTimeOffset DateOfManufacture => MachineProduces.DateOfManufacture;

        [Display]
        [GridDisplay]
        //public string Note => MachineProduces.Note;

        public string Address => MachineWarehouses.Address;

        public virtual MachineProduceEditViewModel MachineProduces { get; set; }

        public virtual MachineWareHouseEditViewModel MachineWarehouses { get; set; }



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
                MachineProduces = MachineProduces.ToModel(),
                MachineWarehouses = MachineWarehouses.ToModel()
            };
            return machines;
        }
    }
}