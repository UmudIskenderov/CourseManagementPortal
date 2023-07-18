using CourseManagementPortalEntities.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class StudentMapper : IBaseMapper<Student, StudentModel>
    {
        public StudentModel Map(Student entity)
        {
            return new StudentModel()
            {
                Id = entity.Id,
                BirthDate = entity.BirthDate,
                Surname = entity.Surname,
                Name = entity.Name,
            };
        }

        public Student Map(StudentModel model)
        {
            return new Student()
            {
                Id = model.Id,
                BirthDate = model.BirthDate,
                Name = model.Name,
                Surname = model.Surname,
            };
        }
    }
}
