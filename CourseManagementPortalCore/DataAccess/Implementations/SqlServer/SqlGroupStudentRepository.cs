using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Implementations.SqlServer
{
    public class SqlGroupStudentRepository : IGroupStudentRepository
    {
        private string _connectionString;
        public SqlGroupStudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<GroupStudent> Get()
        {
            throw new NotImplementedException();
        }

        public GroupStudent GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(GroupStudent entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(GroupStudent entity)
        {
            throw new NotImplementedException();
        }
    }
}
