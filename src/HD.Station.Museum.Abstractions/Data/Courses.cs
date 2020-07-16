using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class Courses
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Credits { get; set; }
        public bool Disabled { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}
