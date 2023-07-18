using CourseManagementPortalEntities.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class TeacherMapper : IBaseMapper<Teacher, TeacherModel>
    {
        public TeacherModel Map(Teacher entity)
        {
            return new TeacherModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                BirthDate = entity.BirthDate,
                Profession = entity.Profession
            };
        }

        public Teacher Map(TeacherModel model)
        {
            return new Teacher()
            {
                Id = model.Id,
                Name = model.Name,
                Surname = model.Surname,
                BirthDate = model.BirthDate,
                Profession = model.Profession
            };
        }
    }
}
