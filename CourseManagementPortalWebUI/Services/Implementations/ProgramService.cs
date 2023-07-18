using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<CourseManagementPortalEntities.Entities.Program, ProgramModel> _mapper;
        public ProgramService(IUnitOfWork db, IBaseMapper<CourseManagementPortalEntities.Entities.Program, ProgramModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public bool Delete(ProgramModel model)
        {
            var program = _mapper.Map(model);
            return _db.ProgramRepository.Delete(program);
        }

        public List<ProgramModel> GetAll()
        {
            List<ProgramModel> programModels = new List<ProgramModel>();
            var programs = _db.ProgramRepository.GetAll();
            var teachers = _db.TeacherRepository.GetAll();
            var courses = _db.CourseRepository.GetAll();
            int no = 1;
            foreach (var program in programs)
            {
                program.Teacher = teachers.FirstOrDefault(x => x.Id == program.TeacherId);
                program.Course = courses.FirstOrDefault(x => x.Id == program.CourseId);
                ProgramModel programModel = _mapper.Map(program);
                programModel.No = no++;
                programModels.Add(programModel);
            }
            return programModels;
        }

        public ProgramModel? GetById(int id)
        {
            var program = _db.ProgramRepository.Get(x => x.Id == id);
            if (program == null)
                return null;
            program.Course = _db.CourseRepository.Get(x => x.Id == program.CourseId);
            program.Teacher = _db.TeacherRepository.Get(x => x.Id == program.TeacherId);
            return _mapper.Map(program);
        }

        public int Save(ProgramModel model)
        {
            var toBeSavedProgram = _mapper.Map(model);

            if (toBeSavedProgram.Id == 0)
            {
                return _db.ProgramRepository.Insert(toBeSavedProgram);
            }
            else
            {
                var existingProgram = _db.ProgramRepository.Get(x => x.Id == model.Id);
                if (existingProgram == null)
                    return 0;
                _db.ProgramRepository.Update(toBeSavedProgram);
                return toBeSavedProgram.Id;
            }
        }
    }
}
