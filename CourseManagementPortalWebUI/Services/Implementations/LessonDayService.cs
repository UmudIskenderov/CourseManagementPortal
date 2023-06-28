using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using System;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class LessonDayService : ILessonDayService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<LessonDay, LessonDayModel> _mapper;

        public LessonDayService(IUnitOfWork db, IBaseMapper<LessonDay, LessonDayModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            return _db.LessonDayRepository.Delete(id);
        }

        public List<LessonDayModel> GetAll()
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.Get();
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.No = no++;
                lessonDayModels.Add(lessonDayModel);
            }
            return lessonDayModels;
        }

        public List<LessonDayModel> GetByDayOfWeek(DayOfWeek dayOfWeek)
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.GetByDayOfWeek((byte) dayOfWeek);
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.No = no++;
                lessonDayModels.Add(lessonDayModel);
            }
            return lessonDayModels;
        }

        public LessonDayModel? GetById(int id)
        {
            LessonDay? lessonDay = _db.LessonDayRepository.GetById(id);
            if (lessonDay == null)
                return null;
            return _mapper.Map(lessonDay);
        }

        public List<LessonDayModel> GetByStudentId(int studentId)
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.GetByStudentId(studentId);
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.No = no++;
                lessonDayModels.Add(lessonDayModel);
            }
            return lessonDayModels;
        }

        public int Save(LessonDayModel model)
        {
            LessonDay toBeSavedLessonday = _mapper.Map(model);

            if (toBeSavedLessonday.Id == 0)
            {
                return _db.LessonDayRepository.Insert(toBeSavedLessonday);
            }
            else
            {
                LessonDay? existingLessonDay = _db.LessonDayRepository.GetById(model.Id);
                if (existingLessonDay == null)
                    return 0;
                _db.LessonDayRepository.Update(toBeSavedLessonday);
                return toBeSavedLessonday.Id;
            }
        }
    }
}
