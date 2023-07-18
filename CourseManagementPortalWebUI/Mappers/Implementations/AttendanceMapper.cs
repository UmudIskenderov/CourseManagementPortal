using CourseManagementPortalEntities.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class AttendanceMapper : IBaseMapper<Attendance, AttendanceModel>
    {
        private readonly IBaseMapper<StudentProgram, StudentProgramModel> _studentProgramMapper;

        public AttendanceMapper(IBaseMapper<StudentProgram, StudentProgramModel> studentProgramMapper)
        {
            _studentProgramMapper = studentProgramMapper;
        }

        public AttendanceModel Map(Attendance entity)
        {
            var attendanceModel = new AttendanceModel()
            {
                Id = entity.Id,
                IsParticipated = entity.IsParticipated,
                Note = entity.Note,
                Date = entity.Date               
            };

            return attendanceModel;
        }

        public Attendance Map(AttendanceModel model)
        {
            var attendance = new Attendance()
            {
                Id = model.Id,
                IsParticipated = model.IsParticipated,
                Note = model.Note,
                Date = model.Date
            };
            if (model.StudentProgram != null)
            {
                attendance.StudentProgramId = model.StudentProgram.Id;
            }

            return attendance;
        }
    }
}
