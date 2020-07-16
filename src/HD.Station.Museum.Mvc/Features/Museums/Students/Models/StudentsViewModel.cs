using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class StudentsViewModel : ViewBase<Students, Guid>
    {
        public StudentsViewModel() { }
        public StudentsViewModel(Students Students)
        {
            Id = Students.Id;
            FirstName = Students.FirstName;
            LastName = Students.LastName;
            BirthDay = Students.BirthDay;
            Disabled = Students.Disabled;
        }
        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display]
        [GridDisplay]
        public string FirstName { get; set; }
        [Display]
        [GridDisplay]
        public string LastName { get; set; }
        [Display]
        [GridDisplay]
        public DateTimeOffset BirthDay { get; set; }

        [Display]
        [GridDisplay]
        public bool Disabled { get; set; }
        [Display]

        public override Students ToModel()
        {
            var Students = new Students
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDay = BirthDay,
                Disabled = Disabled,
            };
            return Students;
        }
    }
}
