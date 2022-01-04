using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTracker.Migrations
{
    public partial class Migr8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones");

            migrationBuilder.AlterColumn<int>(
                name: "ContactID",
                table: "TargetPhones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones");

            migrationBuilder.AlterColumn<int>(
                name: "ContactID",
                table: "TargetPhones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
