using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class Attendance : IEntity
    {
        public int Id { get; set; }
        public StudentProgram? StudentProgram { get; set; }
        public int StudentProgramId => StudentProgram?.Id ?? 0;
        public bool IsParticipated { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
