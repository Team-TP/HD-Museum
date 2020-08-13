using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class Machines
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public bool Disabled { get; set; }

        public Guid? ParentId { get; set; }
        public StageType Stage { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public Machines ParentMachine { get; set; }
        [JsonIgnore]
        public ICollection<Machines> ChildrenMachine { get; set; }

        public virtual MachineProduces MachineProduces { get; set; }

        public virtual MachineWareHouses MachineWarehouses { get; set; }
        public IEnumerable<Guid> CateIds { get; set; }
        public virtual IEnumerable<CategoryMachines> CategoryMachines { get; set; }

    }
}
