using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class _290320240001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JobSteps",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JobSteps");
        }
    }
}
