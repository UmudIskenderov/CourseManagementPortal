using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

namespace CourseManagementPortalWebUI.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly IUnitOfWork _db;
        private readonly IBaseMapper<CourseManagementPortalCore.Domain.Entities.Program, ProgramModel> _mapper;
        public ProgramService(IUnitOfWork db, IBaseMapper<CourseManagementPortalCore.Domain.Entities.Program, ProgramModel> mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public bool Delete(int id)
        {
            return _db.ProgramRepository.Delete(id);
        }

        public List<ProgramModel> GetAll()
        {
            List<ProgramModel> programModels = new List<ProgramModel>();
            var programs = _db.ProgramRepository.Get();
            int no = 1;
            foreach (var program in programs)
            {
                ProgramModel programModel = _mapper.Map(program);
                programModel.No = no++;
                programModels.Add(programModel);
            }
            return programModels;
        }

        public ProgramModel? GetById(int id)
        {
            var program = _db.ProgramRepository.GetById(id);
            if (program == null)
                return null;
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
                var existingProgram = _db.ProgramRepository.GetById(model.Id);
                if (existingProgram == null)
                    return 0;
                _db.ProgramRepository.Update(toBeSavedProgram);
                return toBeSavedProgram.Id;
            }
        }
    }
}
