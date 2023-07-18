using CourseManagementPortalEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalEntities.Entities
{
    public class Attendance : IEntity
    {
        public int Id { get; set; }
        public int StudentProgramId { get; set; }
        public StudentProgram? StudentProgram { get; set; }
        public bool IsParticipated { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
