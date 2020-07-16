using HD.Station.ComponentModel;
using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class EnrollmentsEditViewModel : ViewBase<Enrollments, Guid>
    {
        public EnrollmentsEditViewModel() { }
        public EnrollmentsEditViewModel(Enrollments enrollments)
        {
            if (enrollments != null)
            {
                Id = enrollments.Id;
                CourseId = enrollments.CourseId;
                StudentId = enrollments.StudentId;
                LastName = enrollments.Student?.LastName;
                Title = enrollments.Courses?.Title;
                Grade = enrollments.Grade;
                Disable = enrollments.Disable;
            }
        }
        [Display]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Hidden]
        public Guid CourseId { get; set; }

        [Hidden]
        public Guid StudentId { get; set; }
        [Display]
        [GridDisplay]
        public string Title { get; set; }
        [Display]
        [GridDisplay]
        public string LastName { get; set; }
        [Display]
        [GridDisplay]
        public int Grade { get; set; }
        [Display]
        [GridDisplay]
        public bool Disable { get; set; }
        public List<SelectListItem> Students { set; get; }
        public List<SelectListItem> Courses { set; get; }

        public override Enrollments ToModel()
        {
            var enrollments = new Enrollments
            {
                Id = Id,
                CourseId = CourseId,
                StudentId = StudentId,
                Grade = Grade,
                Disable = Disable,
            };
            return enrollments;
        }
    }
}
