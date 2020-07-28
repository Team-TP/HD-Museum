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
    public class MachineProduceEditViewModel : ViewBase<MachineProduces, Guid>
    {
        public MachineProduceEditViewModel() { }
        public MachineProduceEditViewModel(MachineProduces machineProduce)
        {
            if (machineProduce != null)
            {
                MachineId = machineProduce.MachineId;
                DateOfManufacture = machineProduce.DateOfManufacture;
                DateTest = machineProduce.DateTest;
                State = machineProduce.State;
                Note = machineProduce.Note;
            }
        }
        [Hidden]
        public Guid MachineId { get => base.Id; set => base.Id = value; }

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


        public override MachineProduces ToModel()
        {
            var machineProduce = new MachineProduces
            {
                MachineId = MachineId,
                DateOfManufacture = DateOfManufacture,
                DateTest = DateTest,
                State = State,
                Note = Note
            };
            return machineProduce;
        }
    }
}
