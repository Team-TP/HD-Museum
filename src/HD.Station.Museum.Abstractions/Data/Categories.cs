using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class Categories
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public bool Disabled { get; set; }

        public Guid? ParentId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public Categories ParentCategory { get; set; }

        public ICollection<Categories> ChildrenCategory { get; set; }

        //public ICollection<CategoryMachines> CategoryMachine { get; set; }

    }
}
