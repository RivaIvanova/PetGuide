namespace PetGuide.Data.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VolunteerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ShelterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_AspNetUsers_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_PetEvents_EventId",
                        column: x => x.EventId,
                        principalTable: "PetEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Shelters_ShelterId",
                        column: x => x.ShelterId,
                        principalTable: "Shelters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requests_EventId",
                table: "Requests",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_IsDeleted",
                table: "Requests",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ShelterId",
                table: "Requests",
                column: "ShelterId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_VolunteerId",
                table: "Requests",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
