using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class LessonDay : IEntity
    {
        public int Id { get; set; }
        public StudentProgram? StudentProgram { get; set; }
        public int StudentPrgramId => StudentProgram?.Id ?? 0;
        public DayOfWeek DayOfWeek { get; set; }
    }
}
