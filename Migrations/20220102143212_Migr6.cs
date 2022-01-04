using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTracker.Migrations
{
    public partial class Migr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OldCode",
                table: "TargetPhones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OldCode",
                table: "TargetPhones");
        }
    }
}
