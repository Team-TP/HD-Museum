using HD.Station.ComponentModel;
using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            }
        }
        [Display]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Hidden]
        public string Name { get; set; }
        [Display]
        [GridDisplay]
        public string Description { get; set; }
        [Display]
        [GridDisplay]
        public bool Disable { get; set; }
        [Hidden]
        public Guid? ParentId { get; set; }

        [Display]
        [GridDisplay]
        public MachineType Stage { get; set; }

        [Display]
        [GridDisplay]
        public int Amount { get; set; }

        [Display]
        [GridDisplay]
        public decimal Price { get; set; }

        [Display]
        [GridDisplay]
        public DateTimeOffset DateOfManufacture { get; set; }

        [Display]
        [GridDisplay]
        public DateTimeOffset DateTest { get; set; }

        [Display]
        [GridDisplay]
        public MachineProduceType State { get; set; }

        [Display]
        [GridDisplay]
        public string Note { get; set; }

        [Display]
        [GridDisplay]
        public string Address { get; set; }
        //[Display]
        //[GridDisplay]
        //public string Note =>  { get; set; }
        [Display]
        [GridDisplay]
        public string Phone { get; set; }
        [Display]
        [GridDisplay]
        public string Email { get; set; }
        [Display]
        public MachineProduces MachineProduce { get; set; }
        [Display]
        public MachineWareHouses MachineWareHouse { get; set; }

        public override Machines ToModel()
        {
            var machines = new Machines
            {

            };
            return machines;
        }
    }
}
