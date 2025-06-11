using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Specialty = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.TrainerId);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    TrainingPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanName = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.TrainingPlanId);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    DateJoined = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TrainerId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingPlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.TraineeId);
                    table.ForeignKey(
                        name: "FK_Trainees_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "TrainerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainees_TrainingPlans_TrainingPlanId",
                        column: x => x.TrainingPlanId,
                        principalTable: "TrainingPlans",
                        principalColumn: "TrainingPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgressRecords",
                columns: table => new
                {
                    ProgressRecordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecordDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    Weight = table.Column<double>(type: "REAL", nullable: false),
                    Bmi = table.Column<double>(type: "REAL", nullable: false),
                    TraineeId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgressRecords", x => x.ProgressRecordId);
                    table.ForeignKey(
                        name: "FK_ProgressRecords_Trainees_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "Trainees",
                        principalColumn: "TraineeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "TrainerId", "Name", "Specialty" },
                values: new object[,]
                {
                    { -2, "Jane Smith", "Cardio & Endurance" },
                    { -1, "John Doe", "Strength Training" }
                });

            migrationBuilder.InsertData(
                table: "TrainingPlans",
                columns: new[] { "TrainingPlanId", "Description", "PlanName" },
                values: new object[,]
                {
                    { -2, "High-intensity cardio workouts", "Cardio Blast" },
                    { -1, "Basic strength exercises", "Beginner Strength" }
                });

            migrationBuilder.InsertData(
                table: "Trainees",
                columns: new[] { "TraineeId", "DateJoined", "FullName", "Password", "Role", "TrainerId", "TrainingPlanId", "Username" },
                values: new object[,]
                {
                    { -6, new DateTime(2025, 3, 26, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1491), "Admin Admin", "password123", "Administrator", -1, -1, "admin" },
                    { -5, new DateTime(2025, 3, 26, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1489), "Samuel Brown", "password123", "User", -1, -1, "samuelbrown" },
                    { -4, new DateTime(2025, 4, 30, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1484), "Emily Clark", "password123", "User", -2, -2, "emilyclark" },
                    { -3, new DateTime(2025, 3, 10, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1482), "David Lee", "password123", "User", -1, -1, "davidlee" },
                    { -2, new DateTime(2025, 5, 10, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1480), "Maria Garcia", "password123", "User", -2, -2, "mariagarcia" },
                    { -1, new DateTime(2025, 4, 10, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1469), "Alex Johnson", "password123", "User", -1, -1, "alexjohnson" }
                });

            migrationBuilder.InsertData(
                table: "ProgressRecords",
                columns: new[] { "ProgressRecordId", "Bmi", "Notes", "RecordDate", "TraineeId", "Weight" },
                values: new object[,]
                {
                    { -3, 22.0, "Initial assessment", new DateTime(2025, 5, 31, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1566), -2, 68.0 },
                    { -2, 24.199999999999999, "First-week progress", new DateTime(2025, 6, 3, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1565), -1, 74.0 },
                    { -1, 24.5, "Initial assessment", new DateTime(2025, 5, 27, 7, 44, 17, 909, DateTimeKind.Utc).AddTicks(1562), -1, 75.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProgressRecords_TraineeId",
                table: "ProgressRecords",
                column: "TraineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_TrainerId",
                table: "Trainees",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainees_TrainingPlanId",
                table: "Trainees",
                column: "TrainingPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressRecords");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "TrainingPlans");
        }
    }
}
