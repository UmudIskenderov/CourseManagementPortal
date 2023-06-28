using CourseManagementPortalCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Interfaces
{
    public interface ITeacherRepository : IEntityRepository<Teacher>
    {
    }
}
