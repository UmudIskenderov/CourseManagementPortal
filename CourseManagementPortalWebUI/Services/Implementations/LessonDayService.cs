using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
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
        private readonly IStudentProgramService _studentProgramService;

        public LessonDayService(IUnitOfWork db, IBaseMapper<LessonDay, LessonDayModel> mapper, IStudentProgramService studentProgramService)
        {
            _db = db;
            _mapper = mapper;
            _studentProgramService = studentProgramService;
        }

        public bool Delete(LessonDayModel model)
        {
            var lessonDay = _mapper.Map(model);
            return _db.LessonDayRepository.Delete(lessonDay);
        }

        public List<LessonDayModel> GetAll()
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.GetAll();
            var studentPrograms = _studentProgramService.GetAll();
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.StudentProgram = studentPrograms.FirstOrDefault(x => x.Id == lessonDay.StudentProgramId);
                lessonDayModel.No = no++;
                lessonDayModels.Add(lessonDayModel);
            }
            return lessonDayModels;
        }

        public List<LessonDayModel> GetByDayOfWeek(DayOfWeek dayOfWeek)
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.GetAll(x=>x.DayOfWeek == (byte)dayOfWeek);
            var studentPrograms = _studentProgramService.GetAll();
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.StudentProgram = studentPrograms.FirstOrDefault(x => x.Id == lessonDay.StudentProgramId);
                lessonDayModel.No = no++;
                lessonDayModels.Add(lessonDayModel);
            }
            return lessonDayModels;
        }

        public LessonDayModel? GetById(int id)
        {
            LessonDay? lessonDay = _db.LessonDayRepository.Get(x=>x.Id == id);
            if (lessonDay == null)
                return null;
            var lessonDayModel = _mapper.Map(lessonDay);
            lessonDayModel.StudentProgram = _studentProgramService.GetById(lessonDay.StudentProgramId);
            return lessonDayModel;
        }

        public List<LessonDayModel> GetByStudentId(int studentId)
        {
            List<LessonDayModel> lessonDayModels = new List<LessonDayModel>();
            List<LessonDay> LessonDays = _db.LessonDayRepository.GetAll(x=>x.StudentProgramId == studentId);
            var studentPrograms = _studentProgramService.GetAll();
            int no = 1;
            foreach (LessonDay lessonDay in LessonDays)
            {
                LessonDayModel lessonDayModel = _mapper.Map(lessonDay);
                lessonDayModel.StudentProgram = studentPrograms.FirstOrDefault(x => x.Id == lessonDay.StudentProgramId);
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
                LessonDay? existingLessonDay = _db.LessonDayRepository.Get(x=>x.Id == model.Id);
                if (existingLessonDay == null)
                    return 0;
                _db.LessonDayRepository.Update(toBeSavedLessonday);
                return toBeSavedLessonday.Id;
            }
        }
    }
}
