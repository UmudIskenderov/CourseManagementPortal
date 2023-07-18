using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalEntities.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Attendance, AttendanceModel> _mapper;
        private readonly IStudentProgramService _studentProgramService;
        public AttendanceService(IUnitOfWork db, IBaseMapper<Attendance, AttendanceModel> mapper, IStudentProgramService studentProgramService)
        {
            _db = db;
            _mapper = mapper;
            _studentProgramService = studentProgramService;
        }
        public bool Delete(AttendanceModel model)
        {
            var attendance = _mapper.Map(model);
            return _db.AttendanceRepository.Delete(attendance);
        }

        public List<AttendanceModel> GetAll()
        {
            List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
            List<Attendance> attendances = _db.AttendanceRepository.GetAll();
            var studentPrograms = _studentProgramService.GetAll();
            int no = 1;
            foreach (Attendance attendance in attendances)
            {
                AttendanceModel attendanceModel = _mapper.Map(attendance);
                attendanceModel.StudentProgram = studentPrograms.FirstOrDefault(x => x.Id == attendance.StudentProgramId);
                attendanceModel.No = no++;
                attendanceModels.Add(attendanceModel);
            }
            return attendanceModels;
        }

        public AttendanceModel? GetById(int id)
        {
            Attendance? attendance = _db.AttendanceRepository.Get(x=>x.Id == id);
            if (attendance == null)
                return null;
            var attendanceModel = _mapper.Map(attendance);
            attendanceModel.StudentProgram = _studentProgramService.GetById(attendance.StudentProgramId);
            return attendanceModel;
        }

        public List<AttendanceModel> GetByStudentId(int studentId)
        {
            List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
            List<Attendance> attendances = _db.AttendanceRepository.GetAll(x=>x.StudentProgramId == studentId);
            var studentPrograms = _studentProgramService.GetAll();
            int no = 1;
            foreach (Attendance attendance in attendances)
            {
                AttendanceModel attendanceModel = _mapper.Map(attendance);
                attendanceModel.StudentProgram = studentPrograms.FirstOrDefault(x => x.Id == attendance.StudentProgramId);
                attendanceModel.No = no++;
                attendanceModels.Add(attendanceModel);
            }
            return attendanceModels;
        }

        public int Save(AttendanceModel model)
        {
            Attendance toBeSavedAttendance = _mapper.Map(model);

            if (toBeSavedAttendance.Id == 0)
            {
                toBeSavedAttendance.Date = DateTime.Now;
                return _db.AttendanceRepository.Insert(toBeSavedAttendance);
            }
            else
            {
                Attendance? existingAttendance = _db.AttendanceRepository.Get(x => x.Id == model.Id);
                if (existingAttendance == null)
                    return 0;

                toBeSavedAttendance.Date = existingAttendance.Date;
                _db.AttendanceRepository.Update(toBeSavedAttendance);
                return toBeSavedAttendance.Id;
            }
        }
    }
}
