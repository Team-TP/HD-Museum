using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class CoursesViewModel : ViewBase<Courses, Guid>
    {
        public CoursesViewModel() { }
        public CoursesViewModel(Courses courses)
        {
            Id = courses.Id;
            Title = courses.Title;
            Credits = courses.Credits;
            Disabled = courses.Disabled;
        }
        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public string Title { get; set; }
        [Display]
        [GridDisplay]
        public string Credits { get; set; }
        [Display]
        [GridDisplay]
        public bool Disabled { get; set; }
        [Display]

        public override Courses ToModel()
        {
            var courses = new Courses
            {
                Id = Id,
                Title = Title,
                Credits = Credits,
                Disabled = Disabled,
            };
            return courses;
        }
    }
}
