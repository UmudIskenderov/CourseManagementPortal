using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class Program : IEntity
    {
        public int Id { get; set; }
        public Course? Course { get; set; }
        public Teacher? Teacher { get; set; }
        public int CourseId => Course?.Id ?? 0;
        public int TeacherId => Teacher?.Id ?? 0;
    }
}
