using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleFleetManager.Migrations
{
    /// <inheritdoc />
    public partial class _150320240001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TurningRadius",
                table: "TebConfigs",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TurningRadius",
                table: "TebConfigs");
        }
    }
}
