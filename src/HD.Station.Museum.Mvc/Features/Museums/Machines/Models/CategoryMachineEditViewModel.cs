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
    public class CategoryMachineEditViewModel : ViewBase<CategoryMachines, Guid>
    {
        public CategoryMachineEditViewModel() { }
        public CategoryMachineEditViewModel(CategoryMachines categoryMachines)
        {
            if (categoryMachines != null)
            {
                Id = categoryMachines.Id;
                MachineId = categoryMachines.MachineId;
                CategoryId = categoryMachines.CategoryId;
            }
        }

        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public Guid MachineId { get; set; }
        [Display]
        [GridDisplay]
        public Guid CategoryId { get; set; }

        public override CategoryMachines ToModel()
        {
            var categoryMachines = new CategoryMachines
            {
                Id = Id,
                MachineId = MachineId,
                CategoryId = CategoryId,
        };
            return categoryMachines;
        }
    }
}