using CourseManagementPortalEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalEntities.Entities
{
    public class LessonDay : IEntity
    {
        public int Id { get; set; }
        public int StudentProgramId { get; set; }
        public StudentProgram? StudentProgram { get; set; }
        public byte DayOfWeek { get; set; }
    }
}
