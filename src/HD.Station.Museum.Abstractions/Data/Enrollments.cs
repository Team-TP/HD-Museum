using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Museum
{
    public class Enrollments
    {
        [Key]
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }

        public Guid StudentId { get; set; }
        public int Grade { get; set; }
        public bool Disable { get; set; }
        
        public virtual Students Student { get; set; }
        public virtual Courses Courses { get; set; }
    }
}
