﻿using HD.Station.ComponentModel;
using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
                if (machines.CateIds.Count() > 0)
                {
                    CateIds = machines.CateIds;
                }
                else
                {
                    CateIds = null;
                }
                /*CateIds = machines.CategoryMachines.Select(a => a.CategoryId).ToArray();*/ // cách 1 để đọc categoriId
                CategoryMachines = machines.CategoryMachines;
                //MachineProduce = machines.MachineProduces;
                //MachineWarehouse = machines.MachineWarehouses;
                if (machines.MachineProduces != null) { MachineProduce = new MachineProduceEditViewModel(machines.MachineProduces); }
                if (machines.MachineWarehouses != null) { MachineWarehouse = new MachineWareHouseEditViewModel(machines.MachineWarehouses); }
            }
        }

        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Required]       
        [Display]
        [GridDisplay]
        public string Name { get; set; }
        [Display]
        [GridDisplay]
        public string Description { get; set; }
        [Display]
        [GridDisplay]
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
        [GridDisplay]
        [Display]       
        public IEnumerable<Guid> CateIds { get; set; }

        //public MachineProduces MachineProduce { get; set; }

        //public MachineWareHouses MachineWarehouse { get; set; }

        public MachineProduceEditViewModel MachineProduce { get; set; }

        public MachineWareHouseEditViewModel MachineWarehouse { get; set; }
        public virtual IEnumerable<CategoryMachines> CategoryMachines { get; set; }



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
                MachineProduces = MachineProduce?.ToModel(),
                MachineWarehouses = MachineWarehouse?.ToModel()
            };
            return machines;
        }
    }
}