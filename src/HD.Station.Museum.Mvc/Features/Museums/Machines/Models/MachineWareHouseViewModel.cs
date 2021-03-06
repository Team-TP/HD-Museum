﻿using HD.Station.ComponentModel;
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
    public class MachineWareHouseViewModel : ViewBase<MachineWareHouses, Guid>
    {
        public MachineWareHouseViewModel() { }
        public MachineWareHouseViewModel(MachineWareHouses machineWareHouses)
        {
            if (machineWareHouses != null)
            {
                MachineId = machineWareHouses.MachineId;
                Address = machineWareHouses.Address;
                Note = machineWareHouses.Note;
                Phone = machineWareHouses.Phone;
                Email = machineWareHouses.Email;
            }
        }

        [Hidden]
        public Guid MachineId { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public string Address { get; set; }
        [Display]
        [GridDisplay]
        public string Note { get; set; }
        [Display]
        [GridDisplay]
        public string Phone { get; set; }
        [Display]
        [GridDisplay]
        public string Email { get; set; }



        public override MachineWareHouses ToModel()
        {
            var machineWareHouses = new MachineWareHouses
            {
                MachineId = MachineId,
                Address = Address,
                Note = Note,
                Phone = Phone,
                Email = Email,
            };
            return machineWareHouses;
        }
    }
}