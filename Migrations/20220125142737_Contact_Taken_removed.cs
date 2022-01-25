using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTrackerOnline.Migrations
{
    public partial class Contact_Taken_removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Taken",
                table: "Contacts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Taken",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
