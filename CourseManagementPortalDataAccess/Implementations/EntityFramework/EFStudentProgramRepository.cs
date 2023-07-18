﻿using CourseManagementPortalEntities.Entities;
using CourseManagementPortalCore.Utils;
using CourseManagementPortalDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class EFStudentProgramRepository : EfEntityRepositoryBase<StudentProgram, CourseManagementPortalContext>, IStudentProgramRepository
    {
    }
}
