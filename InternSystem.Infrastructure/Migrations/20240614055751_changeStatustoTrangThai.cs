using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeStatustoTrangThai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "InternInfo");

            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "InternInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "InternInfo");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InternInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
