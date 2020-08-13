using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class CategoryMachines
    {
        public CategoryMachines(Guid machineId, Guid categoryId)
        {
            MachineId = machineId;
            CategoryId = categoryId;
        }
        public CategoryMachines() { }
        [Key]
        public Guid Id { get; set; }

        public Guid MachineId { get; set; }
        public Guid CategoryId { get; set; }

        [JsonIgnore]
        public virtual Categories Category { get; set; }
        public virtual Machines Machine { get; set; }

    }
}
