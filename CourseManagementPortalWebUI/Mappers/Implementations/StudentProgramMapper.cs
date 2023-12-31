﻿using CourseManagementPortalEntities.Entities;
using CourseManagementPortalEntities.Interfaces;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Mappers.Implementations
{
    public class StudentProgramMapper : IBaseMapper<StudentProgram, StudentProgramModel>
    {
        private readonly IBaseMapper<Course, CourseModel> _courseMapper;
        private readonly IBaseMapper<Teacher, TeacherModel> _teacherMapper;
        private readonly IBaseMapper<Student, StudentModel> _studentMapper;

        public StudentProgramMapper(IBaseMapper<Course, CourseModel> courseMapper, IBaseMapper<Teacher, TeacherModel> teacherMapper,
                                    IBaseMapper<Student, StudentModel> studentMapper)
        {
            _courseMapper = courseMapper;
            _teacherMapper = teacherMapper;
            _studentMapper = studentMapper;
        }
        public StudentProgramModel Map(StudentProgram entity)
        {
            StudentProgramModel studentProgramModel = new StudentProgramModel()
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate                
            };
            if(entity.Teacher != null)
            {
                studentProgramModel.Teacher = _teacherMapper.Map(entity.Teacher);
            }
            if (entity.Student != null)
            {
                studentProgramModel.Student = _studentMapper.Map(entity.Student);
            }
            if (entity.Course != null)
            {
                studentProgramModel.Course = _courseMapper.Map(entity.Course);
            }

            return studentProgramModel;
        }

        public StudentProgram Map(StudentProgramModel model)
        {
            StudentProgram studentProgram = new StudentProgram()
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
            if (model.Student != null)
            {
                studentProgram.StudentId = model.Student.Id;
            }
            if (model.Teacher != null)
            {
                studentProgram.TeacherId = model.Teacher.Id;
            }
            if (model.Course != null)
            {
                studentProgram.CourseId = model.Course.Id;
            }

            return studentProgram;
        }
    }
}
