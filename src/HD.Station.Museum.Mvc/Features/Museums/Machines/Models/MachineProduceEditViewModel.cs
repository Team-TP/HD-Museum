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
    public class MachineProduceEditViewModel : ViewBase<MachineProduces, Guid>
    {
        public MachineProduceEditViewModel() { }
        public MachineProduceEditViewModel(MachineProduces machineProduces)
        {
            if (machineProduces != null)
            {
                MachineId = machineProduces.MachineId;
                DateOfManufacture = machineProduces.DateOfManufacture;
                DateTest = machineProduces.DateTest;
                State = machineProduces.State;
                Note = machineProduces.Note;
            }
        }

        [Hidden]
        public Guid MachineId { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]        
        public DateTimeOffset? DateOfManufacture { get; set; }
        [Display]
        [GridDisplay]
        public DateTimeOffset? DateTest { get; set; }
        [Display]
        [GridDisplay]
        public StateType State { get; set; }
        [Display]
        [GridDisplay]
        public string Note { get; set; }



        public override MachineProduces ToModel()
        {
            var machineProduces = new MachineProduces
            {
                MachineId = MachineId,
                DateOfManufacture = DateOfManufacture,
                DateTest = DateTest,
                State = State,
                Note = Note,
        };
            return machineProduces;
        }
    }
}