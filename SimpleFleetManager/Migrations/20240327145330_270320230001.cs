using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class _270320230001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSteps_Jobs_JobId",
                table: "JobSteps");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobSteps",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAssigned",
                table: "JobSteps",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSteps_Jobs_JobId",
                table: "JobSteps",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSteps_Jobs_JobId",
                table: "JobSteps");

            migrationBuilder.DropColumn(
                name: "IsAssigned",
                table: "JobSteps");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "JobSteps",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSteps_Jobs_JobId",
                table: "JobSteps",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id");
        }
    }
}
