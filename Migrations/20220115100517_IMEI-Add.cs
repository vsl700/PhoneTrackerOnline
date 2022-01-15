using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTrackerOnline.Migrations
{
    public partial class IMEIAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_CallerUsers_UserID",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsAlreadyTaken",
                table: "TargetPhones");

            migrationBuilder.AddColumn<long>(
                name: "IMEI",
                table: "TargetPhones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMEI",
                table: "TargetPhones");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlreadyTaken",
                table: "TargetPhones",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_CallerUsers_UserID",
                table: "Contacts",
                column: "UserID",
                principalTable: "CallerUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
