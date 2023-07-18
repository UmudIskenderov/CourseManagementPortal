using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class StudentProgramService : IStudentProgramService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<StudentProgram, StudentProgramModel> _mapper;
        public StudentProgramService(IUnitOfWork db, IBaseMapper<StudentProgram, StudentProgramModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public bool Delete(StudentProgramModel model)
        {
            var studentProgram = _mapper.Map(model);
            return _db.StudentProgramRepository.Delete(studentProgram);
        }

        public List<StudentProgramModel> GetAll()
        {
            List<StudentProgramModel> studentProgramModels = new List<StudentProgramModel>();
            List<StudentProgram> studentPrograms = _db.StudentProgramRepository.GetAll();
            var students = _db.StudentRepository.GetAll();
            var teachers = _db.TeacherRepository.GetAll();
            var courses = _db.CourseRepository.GetAll();
            int no = 1;
            foreach (StudentProgram studentProgram in studentPrograms)
            {
                studentProgram.Student = students.FirstOrDefault(x => x.Id == studentProgram.StudentId);
                studentProgram.Teacher = teachers.FirstOrDefault(x => x.Id == studentProgram.TeacherId);
                studentProgram.Course = courses.FirstOrDefault(x => x.Id == studentProgram.CourseId);
                StudentProgramModel studentProgramModel = _mapper.Map(studentProgram);
                studentProgramModel.No = no++;
                studentProgramModels.Add(studentProgramModel);
            }
            return studentProgramModels;
        }

        public StudentProgramModel? GetById(int id)
        {
            StudentProgram? studentProgram = _db.StudentProgramRepository.Get(x=>x.Id == id);
            if (studentProgram == null)
                return null;
            studentProgram.Student = _db.StudentRepository.Get(x => x.Id == studentProgram.StudentId);
            studentProgram.Teacher = _db.TeacherRepository.Get(x => x.Id == studentProgram.TeacherId);
            studentProgram.Course = _db.CourseRepository.Get(x => x.Id == studentProgram.CourseId);
            return _mapper.Map(studentProgram);
        }

        public int Save(StudentProgramModel model)
        {
            StudentProgram toBeSavedStudentProgram = _mapper.Map(model);

            if (toBeSavedStudentProgram.Id == 0)
            {
                return _db.StudentProgramRepository.Insert(toBeSavedStudentProgram);
            }
            else
            {
                StudentProgram? existingStudentProgram = _db.StudentProgramRepository.Get(x => x.Id == model.Id);
                if (existingStudentProgram == null)
                    return 0;
                _db.StudentProgramRepository.Update(toBeSavedStudentProgram);
                return toBeSavedStudentProgram.Id;
            }
        }
    }
}
