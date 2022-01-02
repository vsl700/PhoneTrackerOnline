using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTracker.Migrations
{
    public partial class Migr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneOldCode",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CurrentLat",
                table: "TargetPhones");

            migrationBuilder.DropColumn(
                name: "CurrentLong",
                table: "TargetPhones");

            migrationBuilder.RenameColumn(
                name: "OldCode",
                table: "TargetPhones",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TargetPhoneOldCode",
                table: "Location",
                newName: "TargetPhoneID");

            migrationBuilder.RenameIndex(
                name: "IX_Location_TargetPhoneOldCode",
                table: "Location",
                newName: "IX_Location_TargetPhoneID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneID",
                table: "Location",
                column: "TargetPhoneID",
                principalTable: "TargetPhones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneID",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TargetPhones",
                newName: "OldCode");

            migrationBuilder.RenameColumn(
                name: "TargetPhoneID",
                table: "Location",
                newName: "TargetPhoneOldCode");

            migrationBuilder.RenameIndex(
                name: "IX_Location_TargetPhoneID",
                table: "Location",
                newName: "IX_Location_TargetPhoneOldCode");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneOldCode",
                table: "Location",
                column: "TargetPhoneOldCode",
                principalTable: "TargetPhones",
                principalColumn: "OldCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
