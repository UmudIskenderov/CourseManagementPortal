using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Student, StudentModel> _mapper;
        public StudentService(IUnitOfWork db, IBaseMapper<Student, StudentModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            return _db.StudentRepository.Delete(id);
        }

        public List<StudentModel> GetAll()
        {
            List<StudentModel> studentModels = new List<StudentModel>();
            List<Student> students = _db.StudentRepository.Get();
            int no = 1;
            foreach (Student student in students)
            {
                StudentModel studentModel = _mapper.Map(student);
                studentModel.No = no++;
                studentModels.Add(studentModel);
            }
            return studentModels;
        }

        public StudentModel? GetById(int id)
        {
            Student? student = _db.StudentRepository.GetById(id);
            if (student == null)
                return null;
            return _mapper.Map(student);
        }

        public int Save(StudentModel model)
        {
            Student toBeSavedStudent = _mapper.Map(model);
            toBeSavedStudent.ModificationTime = DateTime.Now;

            if (toBeSavedStudent.Id == 0)
            {
                toBeSavedStudent.CreationTime = DateTime.Now;
                return _db.StudentRepository.Insert(toBeSavedStudent);
            }
            else
            {
                Student? existingStudent = _db.StudentRepository.GetById(model.Id);
                if (existingStudent == null)
                    return 0;
                toBeSavedStudent.CreationTime = existingStudent.CreationTime;
                _db.StudentRepository.Update(toBeSavedStudent);
                return toBeSavedStudent.Id;
            }
        }
    }
}
