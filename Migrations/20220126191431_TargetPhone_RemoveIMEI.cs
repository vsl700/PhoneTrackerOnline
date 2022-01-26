using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTrackerOnline.Migrations
{
    public partial class TargetPhone_RemoveIMEI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IMEI",
                table: "TargetPhones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IMEI",
                table: "TargetPhones",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
