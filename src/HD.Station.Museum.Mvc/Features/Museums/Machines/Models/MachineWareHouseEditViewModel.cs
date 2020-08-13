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
    public class MachineWareHouseEditViewModel : ViewBase<MachineWareHouses, Guid>
    {
        public MachineWareHouseEditViewModel() { }
        public MachineWareHouseEditViewModel(MachineWareHouses machineWareHouses)
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
        [Required]
        //[RegularExpression(@"[0-9]{10}")]
        [StringLength(10,ErrorMessage = "Số điện thoại phải là 10 số", MinimumLength = 10)]
        public string Phone { get; set; }
        [Display]
        [GridDisplay]
        [Required]
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