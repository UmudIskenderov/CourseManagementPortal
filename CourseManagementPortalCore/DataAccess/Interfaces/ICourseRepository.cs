﻿using CourseManagementPortalCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Interfaces
{
    public interface ICourseRepository : IEntityRepository<Course>
    {
        byte GetDuration(int id);
    }
}
