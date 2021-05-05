using Microsoft.EntityFrameworkCore.Migrations;

namespace Noskito.Database.Migrations
{
    public partial class ModifiedSomeCharacterProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                "Level",
                "characters",
                "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                "HeroLevel",
                "characters",
                "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "JobLevel",
                "characters",
                "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "HeroLevel",
                "characters");

            migrationBuilder.DropColumn(
                "JobLevel",
                "characters");

            migrationBuilder.AlterColumn<byte>(
                "Level",
                "characters",
                "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}