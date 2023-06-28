using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalCore.Domain.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class ProgramMapper : IBaseMapper<CourseManagementPortalCore.Domain.Entities.Program, ProgramModel>
    {
        private readonly IBaseMapper<Course, CourseModel> _courseMapper;
        private readonly IBaseMapper<Teacher, TeacherModel> _teacherMapper;
        public ProgramMapper(IBaseMapper<Course, CourseModel> courseMapper, IBaseMapper<Teacher, TeacherModel> teacherMapper)
        {
            _courseMapper = courseMapper;
            _teacherMapper = teacherMapper;
        }

        public ProgramModel Map(CourseManagementPortalCore.Domain.Entities.Program entity)
        {
            ProgramModel programModel = new ProgramModel()
            {
                Id = entity.Id
            };
            if(entity.Teacher != null)
            {
                programModel.Teacher = _teacherMapper.Map(entity.Teacher);
            }
            if (entity.Course != null)
            {
                programModel.Course = _courseMapper.Map(entity.Course);
            }

            return programModel;
        }

        public CourseManagementPortalCore.Domain.Entities.Program Map(ProgramModel model)
        {
            var program = new CourseManagementPortalCore.Domain.Entities.Program()
            {
                Id = model.Id
            };
            if (model.Teacher != null)
            {
                program.Teacher = _teacherMapper.Map(model.Teacher);
            }
            if (model.Course != null)
            {
                program.Course = _courseMapper.Map(model.Course);
            }

            return program;
        }
    }
}
