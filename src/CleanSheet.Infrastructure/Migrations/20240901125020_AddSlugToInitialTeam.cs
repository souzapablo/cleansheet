using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddSlugToInitialTeam : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Slug",
            table: "InitialTeams",
            type: "varchar(80)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateIndex(
            name: "IX_InitialTeams_Slug",
            table: "InitialTeams",
            column: "Slug",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_InitialTeams_Slug",
            table: "InitialTeams");

        migrationBuilder.DropColumn(
            name: "Slug",
            table: "InitialTeams");
    }
}
