using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using System.Numerics;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Course, CourseModel> _courseMapper;
        public CourseService(IUnitOfWork db, IBaseMapper<Course, CourseModel> mapper)
        {
            _db = db;
            _courseMapper = mapper;
        }

        public bool Delete(CourseModel model)
        {
            var course = _courseMapper.Map(model);
            return _db.CourseRepository.Delete(course);
        }

        public List<CourseModel> GetAll()
        {
            List<CourseModel> courseModels = new List<CourseModel>();
            List<Course> courses = _db.CourseRepository.GetAll();
            int no = 1;
            foreach (Course course in courses)
            {
                CourseModel courseModel = _courseMapper.Map(course);
                courseModel.No = no++;
                courseModels.Add(courseModel);
            }
            return courseModels;
        }

        public CourseModel? GetById(int id)
        {
            Course? course = _db.CourseRepository.Get(x=>x.Id == id);
            if (course == null)
                return null;
            return _courseMapper.Map(course);
        }

        public CourseModel? GetDuration(int id)
        {
            Course? course = _db.CourseRepository.Get(x=>x.Id == id);
            if (course == null)
                return null;
            return _courseMapper.Map(course);
        }

        public int Save(CourseModel model)
        {
            Course toBeSavedCourse = _courseMapper.Map(model);
            toBeSavedCourse.ModificationTime = DateTime.Now;

            if (toBeSavedCourse.Id == 0)
            {
                toBeSavedCourse.CreationTime = DateTime.Now;
                return _db.CourseRepository.Insert(toBeSavedCourse);
            }
            else
            {
                Course? existingCourse = _db.CourseRepository.Get(x=>x.Id == model.Id);
                if (existingCourse == null) 
                    return 0;
                toBeSavedCourse.CreationTime = existingCourse.CreationTime;
                _db.CourseRepository.Update(toBeSavedCourse);
                return toBeSavedCourse.Id;
            }
        }
    }
}
