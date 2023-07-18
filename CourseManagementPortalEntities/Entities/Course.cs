using CourseManagementPortalEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalEntities.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}
