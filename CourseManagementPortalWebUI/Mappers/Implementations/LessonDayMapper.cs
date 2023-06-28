using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class LessonDayMapper : IBaseMapper<LessonDay, LessonDayModel>
    {
        private readonly IBaseMapper<StudentProgram, StudentProgramModel> _studentProgramMapper;

        public LessonDayMapper(IBaseMapper<StudentProgram, StudentProgramModel> studentProgramMapper)
        {
            _studentProgramMapper = studentProgramMapper;
        }

        public LessonDayModel Map(LessonDay entity)
        {
            var lessonDayModel = new LessonDayModel()
            {
                 DayOfWeek = entity.DayOfWeek,
                  Id = entity.Id                   
            };
            if(entity.StudentProgram != null)
            {
                lessonDayModel.StudentProgram = _studentProgramMapper.Map(entity.StudentProgram);
            }

            return lessonDayModel;
        }

        public LessonDay Map(LessonDayModel model)
        {
            var lessonDay = new LessonDay()
            {
                Id = model.Id,
                DayOfWeek = model.DayOfWeek,
            };
            if(model.StudentProgram != null)
            {
                lessonDay.StudentProgram = _studentProgramMapper.Map(model.StudentProgram);
            }

            return lessonDay;
        }
    }
}
