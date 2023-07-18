using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
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
        public bool Delete(StudentModel model)
        {
            var student = _mapper.Map(model);
            return _db.StudentRepository.Delete(student);
        }

        public List<StudentModel> GetAll()
        {
            List<StudentModel> studentModels = new List<StudentModel>();
            List<Student> students = _db.StudentRepository.GetAll();
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
            Student? student = _db.StudentRepository.Get(x=>x.Id == id);
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
                Student? existingStudent = _db.StudentRepository.Get(x => x.Id == model.Id);
                if (existingStudent == null)
                    return 0;
                toBeSavedStudent.CreationTime = existingStudent.CreationTime;
                _db.StudentRepository.Update(toBeSavedStudent);
                return toBeSavedStudent.Id;
            }
        }
    }
}
