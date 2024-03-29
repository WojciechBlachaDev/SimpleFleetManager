using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class _290320240002 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSteps_Locations_LocationId",
                table: "JobSteps");

            migrationBuilder.DropIndex(
                name: "IX_JobSteps_LocationId",
                table: "JobSteps");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "JobSteps");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobSteps",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<double>(
                name: "LocR",
                table: "JobSteps",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LocX",
                table: "JobSteps",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LocY",
                table: "JobSteps",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocR",
                table: "JobSteps");

            migrationBuilder.DropColumn(
                name: "LocX",
                table: "JobSteps");

            migrationBuilder.DropColumn(
                name: "LocY",
                table: "JobSteps");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobSteps",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "JobSteps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobSteps_LocationId",
                table: "JobSteps",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSteps_Locations_LocationId",
                table: "JobSteps",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
