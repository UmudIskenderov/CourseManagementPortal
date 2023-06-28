using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using System.Numerics;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<Course, CourseModel> _mapper;
        public CourseService(IUnitOfWork db, IBaseMapper<Course, CourseModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }
               
        public bool Delete(int id)
        {
            return _db.CourseRepository.Delete(id);
        }

        public List<CourseModel> GetAll()
        {
            List<CourseModel> courseModels = new List<CourseModel>();
            List<Course> courses = _db.CourseRepository.Get();
            int no = 1;
            foreach (Course course in courses)
            {
                CourseModel courseModel = _mapper.Map(course);
                courseModel.No = no++;
                courseModels.Add(courseModel);
            }
            return courseModels;
        }

        public CourseModel? GetById(int id)
        {
            Course? course = _db.CourseRepository.GetById(id);
            if (course == null)
                return null;
            return _mapper.Map(course);
        }

        public int Save(CourseModel model)
        {
            Course toBeSavedCourse = _mapper.Map(model);
            toBeSavedCourse.ModificationTime = DateTime.Now;

            if (toBeSavedCourse.Id == 0)
            {
                toBeSavedCourse.CreationTime = DateTime.Now;
                return _db.CourseRepository.Insert(toBeSavedCourse);
            }
            else
            {
                Course? existingCourse = _db.CourseRepository.GetById(model.Id);
                if (existingCourse == null) 
                    return 0;
                toBeSavedCourse.CreationTime = existingCourse.CreationTime;
                _db.CourseRepository.Update(toBeSavedCourse);
                return toBeSavedCourse.Id;
            }
        }
    }
}
