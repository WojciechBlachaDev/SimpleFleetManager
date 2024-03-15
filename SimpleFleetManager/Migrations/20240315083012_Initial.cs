using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forklifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ForkliftIpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    LidarLocIpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    VisionaryIpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    Registrationdate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forklifts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PriorityLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    IsQueued = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRunning = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    CurrentJobStep = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    X = table.Column<double>(type: "REAL", nullable: false),
                    Y = table.Column<double>(type: "REAL", nullable: false),
                    R = table.Column<double>(type: "REAL", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CardTag = table.Column<string>(type: "TEXT", nullable: true),
                    IsLogged = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessLevel = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TebConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ForkliftId = table.Column<int>(type: "INTEGER", nullable: false),
                    MaxVelForward = table.Column<double>(type: "REAL", nullable: false),
                    MaxVelBackward = table.Column<double>(type: "REAL", nullable: false),
                    MaxVelAngular = table.Column<double>(type: "REAL", nullable: false),
                    MaxAccForwardBackward = table.Column<double>(type: "REAL", nullable: false),
                    MaxAccAngular = table.Column<double>(type: "REAL", nullable: false),
                    Wheelbase = table.Column<double>(type: "REAL", nullable: false),
                    GoalLinearTolerance = table.Column<double>(type: "REAL", nullable: false),
                    GoalAngularTolerance = table.Column<double>(type: "REAL", nullable: false),
                    MinObstacleDistance = table.Column<double>(type: "REAL", nullable: false),
                    ObstacleInflationRadius = table.Column<double>(type: "REAL", nullable: false),
                    DynamicObstacleInflationRadius = table.Column<double>(type: "REAL", nullable: false),
                    DtRef = table.Column<double>(type: "REAL", nullable: false),
                    DtHysteresis = table.Column<double>(type: "REAL", nullable: false),
                    IncludeDynamicObstacles = table.Column<bool>(type: "INTEGER", nullable: false),
                    IncludeCostmapObstacles = table.Column<bool>(type: "INTEGER", nullable: false),
                    OscillationRecovery = table.Column<bool>(type: "INTEGER", nullable: false),
                    AllowInitWithBackwardMotion = table.Column<bool>(type: "INTEGER", nullable: false),
                    Save = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TebConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TebConfigs_Forklifts_ForkliftId",
                        column: x => x.ForkliftId,
                        principalTable: "Forklifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    IsRunning = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsDone = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCanceled = table.Column<bool>(type: "INTEGER", nullable: false),
                    JobId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSteps_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobSteps_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobSteps_JobId",
                table: "JobSteps",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSteps_LocationId",
                table: "JobSteps",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TebConfigs_ForkliftId",
                table: "TebConfigs",
                column: "ForkliftId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobSteps");

            migrationBuilder.DropTable(
                name: "TebConfigs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Forklifts");
        }
    }
}
