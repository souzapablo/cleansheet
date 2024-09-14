using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CleanSheet.Infrastructure.Migrations;

/// <inheritdoc />
public partial class MatchesGoalsAndAssists : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Opponents",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TeamId = table.Column<long>(type: "bigint", nullable: false),
                Name = table.Column<string>(type: "varchar(80)", nullable: false),
                Stadium = table.Column<string>(type: "varchar(80)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Opponents", x => x.Id);
                table.ForeignKey(
                    name: "FK_Opponents_Teams_TeamId",
                    column: x => x.TeamId,
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Matches",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TeamId = table.Column<long>(type: "bigint", nullable: false),
                OpponentId = table.Column<long>(type: "bigint", nullable: false),
                Location = table.Column<short>(type: "smallint", nullable: false),
                Date = table.Column<DateOnly>(type: "date", nullable: false),
                HomeGoals = table.Column<short>(type: "smallint", nullable: false),
                AwayGoals = table.Column<short>(type: "smallint", nullable: false),
                Result = table.Column<short>(type: "smallint", nullable: false),
                Competition = table.Column<short>(type: "smallint", nullable: false),
                Stadium = table.Column<string>(type: "varchar(80)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Matches", x => x.Id);
                table.ForeignKey(
                    name: "FK_Matches_Opponents_OpponentId",
                    column: x => x.OpponentId,
                    principalTable: "Opponents",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Matches_Teams_TeamId",
                    column: x => x.TeamId,
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Goals",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                MatchId = table.Column<long>(type: "bigint", nullable: false),
                PlayerScoredId = table.Column<long>(type: "bigint", nullable: true),
                AssistId = table.Column<long>(type: "bigint", nullable: true),
                IsOwnGoal = table.Column<bool>(type: "boolean", nullable: false),
                PlayerId = table.Column<long>(type: "bigint", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Goals", x => x.Id);
                table.ForeignKey(
                    name: "FK_Goals_Matches_MatchId",
                    column: x => x.MatchId,
                    principalTable: "Matches",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Goals_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Goals_Players_PlayerScoredId",
                    column: x => x.PlayerScoredId,
                    principalTable: "Players",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateTable(
            name: "MatchPlayer",
            columns: table => new
            {
                LineupId = table.Column<long>(type: "bigint", nullable: false),
                MatchesId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MatchPlayer", x => new { x.LineupId, x.MatchesId });
                table.ForeignKey(
                    name: "FK_MatchPlayer_Matches_MatchesId",
                    column: x => x.MatchesId,
                    principalTable: "Matches",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MatchPlayer_Players_LineupId",
                    column: x => x.LineupId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Assists",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false),
                PlayerAssistedId = table.Column<long>(type: "bigint", nullable: false),
                PlayerId = table.Column<long>(type: "bigint", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Assists", x => x.Id);
                table.ForeignKey(
                    name: "FK_Assists_Goals_Id",
                    column: x => x.Id,
                    principalTable: "Goals",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Assists_Players_PlayerAssistedId",
                    column: x => x.PlayerAssistedId,
                    principalTable: "Players",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Assists_Players_PlayerId",
                    column: x => x.PlayerId,
                    principalTable: "Players",
                    principalColumn: "Id");
            });

        migrationBuilder.CreateIndex(
            name: "IX_Assists_PlayerAssistedId",
            table: "Assists",
            column: "PlayerAssistedId");

        migrationBuilder.CreateIndex(
            name: "IX_Assists_PlayerId",
            table: "Assists",
            column: "PlayerId");

        migrationBuilder.CreateIndex(
            name: "IX_Goals_MatchId",
            table: "Goals",
            column: "MatchId");

        migrationBuilder.CreateIndex(
            name: "IX_Goals_PlayerId",
            table: "Goals",
            column: "PlayerId");

        migrationBuilder.CreateIndex(
            name: "IX_Goals_PlayerScoredId",
            table: "Goals",
            column: "PlayerScoredId");

        migrationBuilder.CreateIndex(
            name: "IX_Matches_OpponentId",
            table: "Matches",
            column: "OpponentId");

        migrationBuilder.CreateIndex(
            name: "IX_Matches_TeamId",
            table: "Matches",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_MatchPlayer_MatchesId",
            table: "MatchPlayer",
            column: "MatchesId");

        migrationBuilder.CreateIndex(
            name: "IX_Opponents_TeamId",
            table: "Opponents",
            column: "TeamId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Assists");

        migrationBuilder.DropTable(
            name: "MatchPlayer");

        migrationBuilder.DropTable(
            name: "Goals");

        migrationBuilder.DropTable(
            name: "Matches");

        migrationBuilder.DropTable(
            name: "Opponents");
    }
}
