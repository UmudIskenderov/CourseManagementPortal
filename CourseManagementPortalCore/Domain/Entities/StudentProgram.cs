using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class StudentProgram : IEntity
    {
        public int Id { get; set; }
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
        public Course? Course { get; set; }
        public int StudentId => Student?.Id ?? 0;
        public int TeacherId => Teacher?.Id ?? 0;
        public int CourseId => Course?.Id ?? 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
