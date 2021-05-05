using Microsoft.EntityFrameworkCore.Migrations;

namespace Noskito.Database.Migrations
{
    public partial class ModifiedSomeCharacterProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "characters",
                type: "integer",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");

            migrationBuilder.AddColumn<int>(
                name: "HeroLevel",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobLevel",
                table: "characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeroLevel",
                table: "characters");

            migrationBuilder.DropColumn(
                name: "JobLevel",
                table: "characters");

            migrationBuilder.AlterColumn<byte>(
                name: "Level",
                table: "characters",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
