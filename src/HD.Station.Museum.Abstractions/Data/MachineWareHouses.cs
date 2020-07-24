using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class MachineWareHouses
    {
        [Key]
        public Guid MachineId { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
