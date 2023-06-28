using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Teacher, TeacherModel> _mapper;
        public TeacherService(IUnitOfWork db, IBaseMapper<Teacher, TeacherModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public bool Delete(int id)
        {
            return _db.TeacherRepository.Delete(id);
        }

        public List<TeacherModel> GetAll()
        {
            List<TeacherModel> teacherModels = new List<TeacherModel>();
            List<Teacher> teachers = _db.TeacherRepository.Get();
            int no = 1;
            foreach (Teacher teacher in teachers)
            {
                TeacherModel teacherModel = _mapper.Map(teacher);
                teacherModel.No = no++;
                teacherModels.Add(teacherModel);
            }
            return teacherModels;
        }

        public TeacherModel? GetById(int id)
        {
            Teacher? teacher = _db.TeacherRepository.GetById(id);
            if (teacher == null)
                return null;
            return _mapper.Map(teacher);
        }

        public int Save(TeacherModel model)
        {
            Teacher toBeSavedTeacher = _mapper.Map(model);

            if (toBeSavedTeacher.Id == 0)
            {
                return _db.TeacherRepository.Insert(toBeSavedTeacher);
            }
            else
            {
                Teacher? existingTeacher = _db.TeacherRepository.GetById(model.Id);
                if (existingTeacher == null)
                    return 0;
                _db.TeacherRepository.Update(toBeSavedTeacher);
                return toBeSavedTeacher.Id;
            }
        }
    }
}
