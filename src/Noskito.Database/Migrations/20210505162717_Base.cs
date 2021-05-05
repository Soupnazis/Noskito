using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Noskito.Database.Migrations
{
    public partial class Base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "accounts",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>("text", nullable: false),
                    Password = table.Column<string>("text", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_accounts", x => x.Id); });

            migrationBuilder.CreateTable(
                "characters",
                table => new
                {
                    Id = table.Column<long>("bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<long>("bigint", nullable: false),
                    Name = table.Column<string>("text", nullable: false),
                    Level = table.Column<byte>("smallint", nullable: false),
                    Slot = table.Column<byte>("smallint", nullable: false),
                    Job = table.Column<int>("integer", nullable: false),
                    HairColor = table.Column<int>("integer", nullable: false),
                    HairStyle = table.Column<int>("integer", nullable: false),
                    Gender = table.Column<int>("integer", nullable: false),
                    X = table.Column<int>("integer", nullable: false),
                    Y = table.Column<int>("integer", nullable: false),
                    MapId = table.Column<int>("integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_characters", x => x.Id);
                    table.ForeignKey(
                        "FK_characters_accounts_AccountId",
                        x => x.AccountId,
                        "accounts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_characters_AccountId",
                "characters",
                "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "characters");

            migrationBuilder.DropTable(
                "accounts");
        }
    }
}