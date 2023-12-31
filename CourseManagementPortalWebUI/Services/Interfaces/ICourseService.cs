﻿using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface ICourseService
    {
        List<CourseModel> GetAll();
        CourseModel? GetById(int id);
        CourseModel? GetDuration(int id);
        int Save(CourseModel model);
        bool Delete(CourseModel model);
    }
}
