using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseManagementPortalDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duration = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Surname = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Profession = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programs_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentPrograms_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentPrograms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentPrograms_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentProgramId = table.Column<int>(type: "int", nullable: false),
                    IsParticipated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Note = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_StudentPrograms_StudentProgramId",
                        column: x => x.StudentProgramId,
                        principalTable: "StudentPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LessonDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentProgramId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonDays_StudentPrograms_StudentProgramId",
                        column: x => x.StudentProgramId,
                        principalTable: "StudentPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreationTime", "Duration", "ModificationTime", "Name", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)10, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Back-end development", 250m },
                    { 2, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)6, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Front-end development", 200m },
                    { 3, new DateTime(2021, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)8, new DateTime(2021, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "UX/UI design", 300m }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "CreationTime", "ModificationTime", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2003, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Səbuhi", "Məmmədov" },
                    { 2, new DateTime(2003, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "İlham", "Həsənli" },
                    { 3, new DateTime(2003, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ümüd", "İskəndərov" }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "BirthDate", "Name", "Profession", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(1998, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cavid", "Software engineer", "Əliyev" },
                    { 2, new DateTime(2000, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Məhəmməd", "UX/UI designer", "Ağayev" }
                });

            migrationBuilder.InsertData(
                table: "Programs",
                columns: new[] { "Id", "CourseId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "StudentPrograms",
                columns: new[] { "Id", "CourseId", "EndDate", "StartDate", "StudentId", "TeacherId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 2, 3, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2 },
                    { 3, 1, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Attendances",
                columns: new[] { "Id", "Date", "IsParticipated", "Note", "StudentProgramId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Perfect", 3 },
                    { 2, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Perfect", 1 },
                    { 3, new DateTime(2023, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Bad", 3 },
                    { 4, new DateTime(2023, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Bad", 1 },
                    { 5, new DateTime(2023, 7, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Perfect", 2 },
                    { 6, new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 1 },
                    { 7, new DateTime(2023, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "LessonDays",
                columns: new[] { "Id", "DayOfWeek", "StudentProgramId" },
                values: new object[,]
                {
                    { 1, (byte)1, 1 },
                    { 2, (byte)4, 1 },
                    { 3, (byte)2, 2 },
                    { 4, (byte)5, 2 },
                    { 5, (byte)3, 3 },
                    { 6, (byte)6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentProgramId",
                table: "Attendances",
                column: "StudentProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonDays_StudentProgramId",
                table: "LessonDays",
                column: "StudentProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_CourseId",
                table: "Programs",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_TeacherId",
                table: "Programs",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_CourseId",
                table: "StudentPrograms",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_StudentId",
                table: "StudentPrograms",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentPrograms_TeacherId",
                table: "StudentPrograms",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "LessonDays");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "StudentPrograms");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
