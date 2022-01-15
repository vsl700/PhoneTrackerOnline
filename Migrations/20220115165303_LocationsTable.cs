using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTrackerOnline.Migrations
{
    public partial class LocationsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneID",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameIndex(
                name: "IX_Location_TargetPhoneID",
                table: "Locations",
                newName: "IX_Locations_TargetPhoneID");

            migrationBuilder.AlterColumn<int>(
                name: "TargetPhoneID",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_TargetPhones_TargetPhoneID",
                table: "Locations",
                column: "TargetPhoneID",
                principalTable: "TargetPhones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_TargetPhones_TargetPhoneID",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_TargetPhoneID",
                table: "Location",
                newName: "IX_Location_TargetPhoneID");

            migrationBuilder.AlterColumn<int>(
                name: "TargetPhoneID",
                table: "Location",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_TargetPhones_TargetPhoneID",
                table: "Location",
                column: "TargetPhoneID",
                principalTable: "TargetPhones",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
