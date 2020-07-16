using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class Students
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDay { get; set; }
        public bool Disabled { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Enrollments> Enrollments { get; set; }
    }
}
