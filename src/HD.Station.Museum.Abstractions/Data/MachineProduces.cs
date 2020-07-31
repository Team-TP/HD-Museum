using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class MachineProduces
    {
        [Key]
        public Guid MachineId { get; set; }

        public DateTimeOffset DateOfManufacture { get; set; }
        public DateTimeOffset DateTest { get; set; }
        public StateType State { get; set; }
        public string Note { get; set; }
        [JsonIgnore]
        public virtual Machines Machines { get; set; }

    }
}
