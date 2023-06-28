using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Attendance, AttendanceModel> _mapper;
        public AttendanceService(IUnitOfWork db, IBaseMapper<Attendance, AttendanceModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            return _db.AttendanceRepository.Delete(id);
        }

        public List<AttendanceModel> GetAll()
        {
            List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
            List<Attendance> attendances = _db.AttendanceRepository.Get();
            int no = 1;
            foreach (Attendance attendance in attendances)
            {
                AttendanceModel attendanceModel = _mapper.Map(attendance);
                attendanceModel.No = no++;
                attendanceModels.Add(attendanceModel);
            }
            return attendanceModels;
        }

        public AttendanceModel? GetById(int id)
        {
            Attendance? attendance = _db.AttendanceRepository.GetById(id);
            if (attendance == null)
                return null;
            return _mapper.Map(attendance);
        }

        public List<AttendanceModel> GetByStudentId(int studentId)
        {
            List<AttendanceModel> attendanceModels = new List<AttendanceModel>();
            List<Attendance> attendances = _db.AttendanceRepository.GetByStudentId(studentId);
            int no = 1;
            foreach (Attendance attendance in attendances)
            {
                AttendanceModel attendanceModel = _mapper.Map(attendance);
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
                Attendance? existingAttendance = _db.AttendanceRepository.GetById(model.Id);
                if (existingAttendance == null)
                    return 0;

                toBeSavedAttendance.Date = existingAttendance.Date;
                _db.AttendanceRepository.Update(toBeSavedAttendance);
                return toBeSavedAttendance.Id;
            }
        }
    }
}
