# CourseManagementPortal
This is a course management portal. Developed on .Net. It is optimized to work with both MySQL and MsSQL databases.

-Database selection:
Which database we will use should be noted from the text file "CourseManagementPortalWebUI\CourseManagementPortalDataAccess\DbConfig.txt": 
    for MySQL database -> "MYSQL", 
    for MsSQL database -> "SQLSERVER".

-Connect to database:
ConnectionString must be configured in the file "CourseManagementPortalWebUI\CourseManagementPortalDataAccess\Factories\DbFactory.cs" to connect to the database.

Creating a database:
Currently, the project has been migrated to work with a MySQL database.Added mock data for quick testing.
1) Set "CourseManagementPortalDataAccess" project as startup project.
2) Select Tools->NuGet Package Manager->Package Manager Console from the navigation window.
3) In the window that opens, select the "CourseManagementPortalDataAccess" project as the default project.
4) Type Update-Database in the console and press enter.

To migrate the database to MsSQL:
After steps 1, 2 and 3 above are done:
4) Remove-Migration
5) Add-Migration InitialCreate
6) Update-Database is executed.

About project:
Courses page - you can see the list of all courses. Courses can be added, updated and deleted.
Teachers page - you can see the list of all teachers. It is possible to add, update and delete teachers.
Programs page - you can see a list of what courses a teacher can teach. It is possible to add, update and delete programs.
Students page - you can see the list of all students. Students can be added, updated and deleted.
Students' programs - on this page you can see which student started which course with which teacher, the start and end time of the course. It is possible to add, update and delete. On this page, you can see the attendance of students in classes (Detail button) and delete these notes.
Attendances - on this page you can see the list of students who have to attend classes today. If attendance has not been recorded for today, the student's attendance and notes for today can be added.
