using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class CourseMapper : IBaseMapper<Course, CourseModel>
    {
        public CourseModel Map(Course entity)
        {
            return new CourseModel()
            {
                Id = entity.Id,
                Duration = entity.Duration,
                Name = entity.Name,
                Price = entity.Price
            };
        }

        public Course Map(CourseModel model)
        {
            return new Course()
            {
                Id = model.Id,
                Duration = model.Duration,
                Name = model.Name,
                Price = model.Price
            };
        }
    }
}
