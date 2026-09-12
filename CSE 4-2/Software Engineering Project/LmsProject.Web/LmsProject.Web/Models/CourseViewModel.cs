using System.Collections.Generic;
using LmsProject.Domain.Entities;

namespace LmsProject.Web.Models
{
    public class CourseViewModel
    {
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<string> Domains { get; set; } = new List<string>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        // NEW: Enrollment Filter State
        public string SelectedEnrollment { get; set; } = "N/A";

        public string SelectedDomain { get; set; } = "All";
        public string SelectedInstructor { get; set; } = "All";
        public string SearchTerm { get; set; } = string.Empty;
    }
}