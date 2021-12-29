using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneTracker.Migrations
{
    public partial class MigrationForeign3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallerUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallerUsers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taken = table.Column<bool>(type: "bit", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contact_CallerUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "CallerUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TargetPhone",
                columns: table => new
                {
                    OldCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    IsAlreadyTaken = table.Column<bool>(type: "bit", nullable: false),
                    ContactID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetPhone", x => x.OldCode);
                    table.ForeignKey(
                        name: "FK_TargetPhone_CallerUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "CallerUsers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetPhone_Contact_ContactID",
                        column: x => x.ContactID,
                        principalTable: "Contact",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    MarkerColor = table.Column<int>(type: "int", nullable: false),
                    TimeTaken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetPhoneOldCode = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Location_TargetPhone_TargetPhoneOldCode",
                        column: x => x.TargetPhoneOldCode,
                        principalTable: "TargetPhone",
                        principalColumn: "OldCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserID",
                table: "Contact",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_TargetPhoneOldCode",
                table: "Location",
                column: "TargetPhoneOldCode");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPhone_ContactID",
                table: "TargetPhone",
                column: "ContactID");

            migrationBuilder.CreateIndex(
                name: "IX_TargetPhone_UserID",
                table: "TargetPhone",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "TargetPhone");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "CallerUsers");
        }
    }
}
