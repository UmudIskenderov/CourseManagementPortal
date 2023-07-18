using CourseManagementPortalEntities.Entities;
using CourseManagementPortalEntities.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class ProgramMapper : IBaseMapper<CourseManagementPortalEntities.Entities.Program, ProgramModel>
    {
        private readonly IBaseMapper<Course, CourseModel> _courseMapper;
        private readonly IBaseMapper<Teacher, TeacherModel> _teacherMapper;
        public ProgramMapper(IBaseMapper<Course, CourseModel> courseMapper, IBaseMapper<Teacher, TeacherModel> teacherMapper)
        {
            _courseMapper = courseMapper;
            _teacherMapper = teacherMapper;
        }

        public ProgramModel Map(CourseManagementPortalEntities.Entities.Program entity)
        {
            ProgramModel programModel = new ProgramModel()
            {
                Id = entity.Id
            };
            if(entity.Course != null)
            {
                programModel.Course = _courseMapper.Map(entity.Course);
            }
            if(entity.Teacher != null)
            {
                programModel.Teacher = _teacherMapper.Map(entity.Teacher);
            }

            return programModel;
        }

        public CourseManagementPortalEntities.Entities.Program Map(ProgramModel model)
        {
            var program = new CourseManagementPortalEntities.Entities.Program()
            {
                Id = model.Id
            };
            if (model.Teacher != null)
            {
                program.TeacherId = model.Teacher.Id;
            }
            if (model.Course != null)
            {
                program.CourseId = model.Course.Id;
            }

            return program;
        }
    }
}
