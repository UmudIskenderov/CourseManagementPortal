using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
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
        public bool Delete(int id)
        {
            return _db.StudentProgramRepository.Delete(id);
        }

        public List<StudentProgramModel> GetAll()
        {
            List<StudentProgramModel> studentProgramModels = new List<StudentProgramModel>();
            List<StudentProgram> studentPrograms = _db.StudentProgramRepository.Get();
            int no = 1;
            foreach (StudentProgram studentProgram in studentPrograms)
            {
                StudentProgramModel studentProgramModel = _mapper.Map(studentProgram);
                studentProgramModel.No = no++;
                studentProgramModels.Add(studentProgramModel);
            }
            return studentProgramModels;
        }

        public StudentProgramModel? GetById(int id)
        {
            StudentProgram? studentProgram = _db.StudentProgramRepository.GetById(id);
            if (studentProgram == null)
                return null;
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
                StudentProgram? existingStudentProgram = _db.StudentProgramRepository.GetById(model.Id);
                if (existingStudentProgram == null)
                    return 0;
                _db.StudentProgramRepository.Update(toBeSavedStudentProgram);
                return toBeSavedStudentProgram.Id;
            }
        }
    }
}
