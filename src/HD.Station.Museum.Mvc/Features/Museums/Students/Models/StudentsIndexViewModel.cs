using HD.Station.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class StudentsIndexViewModel
    {
        [Hidden]
        public string Filter { get; set; }
        [Display]
        public bool IncludeDisabled { get; set; }
    }
}
