using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Factories
{
    public class DbFactory
    {
        public static void Create(DbContextOptionsBuilder optionsBuilder)
        {
            string vendorName = File.ReadAllText("..\\CourseManagementPortalDataAccess\\DbConfig.txt").ToUpper();
            string connectionString = string.Empty;
            switch (vendorName)
            {
                case "SQLSERVER":
                    connectionString = @"Server = localhost; Database = CourseManagementPortal; Trusted_Connection=True; TrustServerCertificate=true";
                    optionsBuilder.UseSqlServer(connectionString);
                    break;
                case "MYSQL":
                    connectionString = @"Server = localhost; Database = CourseManagementPortal; User ID=root";
                    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                    break;
            }
        }
    }
}
