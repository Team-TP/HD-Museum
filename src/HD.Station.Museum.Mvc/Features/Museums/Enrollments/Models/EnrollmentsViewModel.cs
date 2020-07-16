using HD.Station.ComponentModel.DataAnnotations;
using HD.Station.Museum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HD.Station.Feature.Models
{
    public class EnrollmentsViewModel : ViewBase<Enrollments, Guid>
    {
        public EnrollmentsViewModel() { }
        public EnrollmentsViewModel(Enrollments enrollments)
        {
            if(enrollments != null)
            {
                Id = enrollments.Id;
                CourseId = enrollments.CourseId;
                StudentId = enrollments.StudentId;
                Grade = enrollments.Grade;
                Disable = enrollments.Disable;
                Student = enrollments.Student;
                Courses = enrollments.Courses;
            }

        }
        [Hidden]
        public override Guid Id { get => base.Id; set => base.Id = value; }
        [Display] //the nay la de hien trong trang detail
        [GridDisplay] // co the nay thi no se hien thi trong index. nó có các thuộc tính như name. order ... để thay đổi tên hiển thị ra
        public string CourseName => Courses?.Title;
        [Display]
        [GridDisplay]
        public string LastName => Student?.LastName;
        public Guid CourseId { get; set; }
        [Display]
        [GridDisplay]
        public Guid StudentId { get; set; }
        [Display]
        [GridDisplay]
        public int Grade { get; set; }
        [Display]
        [GridDisplay]
        public bool Disable { get; set; }

        public virtual Students Student { get; set; }
        public virtual Courses Courses { get; set; }
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
