using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class CategoriesViewModel : ViewBase<Categories, Guid>
    {
        public CategoriesViewModel() { }
        public CategoriesViewModel(Categories categories)
        {
            if (categories != null)
            {
                Id = categories.Id;
                Name = categories.Name;
                Description = categories.Description;
                Disabled = categories.Disabled;
                ParentId = categories.ParentId;

            }
        }
        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public string Name { get; set; }
        [Display]
        [GridDisplay]
        public string Description { get; set; }
        [Display]
        [GridDisplay]
        public bool Disabled { get; set; }
        [Display]
        public Guid? ParentId { get; set; }


        public override Categories ToModel()
        {
            var categories = new Categories
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Disabled = Disabled,
                ParentId = ParentId,
            };
            return categories;
        }
    }
}


