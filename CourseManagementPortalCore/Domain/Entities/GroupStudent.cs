using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class GroupStudent : IEntity
    {
        public int Id { get; set; }
        public Student? Student { get; set; }
        public StudentProgram? Group { get; set; }
        public int StudentId => Student?.Id ?? 0;
        public int GroupId => Group?.Id ?? 0;
    }
}
