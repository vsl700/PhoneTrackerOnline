using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTracker.Migrations
{
    public partial class Migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_TargetPhone_TargetPhoneOldCode",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhone_CallerUsers_UserID",
                table: "TargetPhone");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhone_Contact_ContactID",
                table: "TargetPhone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetPhone",
                table: "TargetPhone");

            migrationBuilder.RenameTable(
                name: "TargetPhone",
                newName: "TargetPhones");

            migrationBuilder.RenameIndex(
                name: "IX_TargetPhone_UserID",
                table: "TargetPhones",
                newName: "IX_TargetPhones_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_TargetPhone_ContactID",
                table: "TargetPhones",
                newName: "IX_TargetPhones_ContactID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TargetPhones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLat",
                table: "TargetPhones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentLong",
                table: "TargetPhones",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetPhones",
                table: "TargetPhones",
                column: "OldCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneOldCode",
                table: "Location",
                column: "TargetPhoneOldCode",
                principalTable: "TargetPhones",
                principalColumn: "OldCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhones_CallerUsers_UserID",
                table: "TargetPhones",
                column: "UserID",
                principalTable: "CallerUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneOldCode",
                table: "Location");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhones_CallerUsers_UserID",
                table: "TargetPhones");

            migrationBuilder.DropForeignKey(
                name: "FK_TargetPhones_Contact_ContactID",
                table: "TargetPhones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TargetPhones",
                table: "TargetPhones");

            migrationBuilder.DropColumn(
                name: "CurrentLat",
                table: "TargetPhones");

            migrationBuilder.DropColumn(
                name: "CurrentLong",
                table: "TargetPhones");

            migrationBuilder.RenameTable(
                name: "TargetPhones",
                newName: "TargetPhone");

            migrationBuilder.RenameIndex(
                name: "IX_TargetPhones_UserID",
                table: "TargetPhone",
                newName: "IX_TargetPhone_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_TargetPhones_ContactID",
                table: "TargetPhone",
                newName: "IX_TargetPhone_ContactID");

            migrationBuilder.AlterColumn<int>(
                name: "UserID",
                table: "TargetPhone",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TargetPhone",
                table: "TargetPhone",
                column: "OldCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TargetPhone_TargetPhoneOldCode",
                table: "Location",
                column: "TargetPhoneOldCode",
                principalTable: "TargetPhone",
                principalColumn: "OldCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhone_CallerUsers_UserID",
                table: "TargetPhone",
                column: "UserID",
                principalTable: "CallerUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TargetPhone_Contact_ContactID",
                table: "TargetPhone",
                column: "ContactID",
                principalTable: "Contact",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
