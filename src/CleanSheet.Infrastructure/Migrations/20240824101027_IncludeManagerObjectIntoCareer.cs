using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IncludeManagerObjectIntoCareer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Manager",
                table: "Careers");

            migrationBuilder.AddColumn<string>(
                name: "ManagerFirstName",
                table: "Careers",
                type: "varchar(12)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerLastName",
                table: "Careers",
                type: "varchar(12)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerFirstName",
                table: "Careers");

            migrationBuilder.DropColumn(
                name: "ManagerLastName",
                table: "Careers");

            migrationBuilder.AddColumn<string>(
                name: "Manager",
                table: "Careers",
                type: "varchar(80)",
                nullable: false,
                defaultValue: "");
        }
    }
}
