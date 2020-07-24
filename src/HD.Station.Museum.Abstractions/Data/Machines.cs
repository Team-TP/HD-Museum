﻿using System;
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
        public MachineType Stage { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }

        public ICollection<CategoryMachines> CategoryMachine { get; set; }

    }
}