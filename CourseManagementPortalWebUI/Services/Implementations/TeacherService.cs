using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
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
        public bool Delete(TeacherModel model)
        {
            var teacher = _mapper.Map(model);
            return _db.TeacherRepository.Delete(teacher);
        }

        public List<TeacherModel> GetAll()
        {
            List<TeacherModel> teacherModels = new List<TeacherModel>();
            List<Teacher> teachers = _db.TeacherRepository.GetAll();
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
            Teacher? teacher = _db.TeacherRepository.Get(x=>x.Id == id);
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
                Teacher? existingTeacher = _db.TeacherRepository.Get(x => x.Id == model.Id);
                if (existingTeacher == null)
                    return 0;
                _db.TeacherRepository.Update(toBeSavedTeacher);
                return toBeSavedTeacher.Id;
            }
        }
    }
}
