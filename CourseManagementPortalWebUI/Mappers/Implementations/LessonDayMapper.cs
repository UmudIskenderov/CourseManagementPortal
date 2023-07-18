using CourseManagementPortalEntities.Entities;
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
                DayOfWeek = (DayOfWeek)entity.DayOfWeek,
                Id = entity.Id,
                StudentProgram = new StudentProgramModel()
                {
                    Id = entity.StudentProgramId
                }
            };

            return lessonDayModel;
        }

        public LessonDay Map(LessonDayModel model)
        {
            var lessonDay = new LessonDay()
            {
                Id = model.Id,
                DayOfWeek = (byte)model.DayOfWeek,
            };
            if (model.StudentProgram != null)
            {
                lessonDay.StudentProgramId = model.StudentProgram.Id;
            }

            return lessonDay;
        }
    }
}
