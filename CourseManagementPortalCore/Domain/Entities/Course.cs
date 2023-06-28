using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.Domain.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public Decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}
