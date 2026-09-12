using System.Collections.Generic;
using LmsProject.Domain.Entities;

namespace LmsProject.Web.Models
{
    public class CourseViewModel
    {
        // Lists populated from the database to render on screen
        public List<Course> Courses { get; set; } = new List<Course>();
        public List<string> Domains { get; set; } = new List<string>();
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();

        // Tracks the active user filter selections to maintain state across page reloads
        public string SelectedDomain { get; set; } = "All";
        public string SelectedInstructor { get; set; } = "All";
        public string SearchTerm { get; set; } = string.Empty;
    }
}