namespace PetGuide.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddLocationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "PetEvents");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Shelters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Pets",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "PetEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    District = table.Column<int>(nullable: false),
                    Street = table.Column<string>(maxLength: 50, nullable: false),
                    AdditionalInfo = table.Column<string>(maxLength: 150, nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shelters_LocationId",
                table: "Shelters",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_LocationId",
                table: "Pets",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_PetEvents_LocationId",
                table: "PetEvents",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_IsDeleted",
                table: "Locations",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_PetEvents_Locations_LocationId",
                table: "PetEvents",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelters_Locations_LocationId",
                table: "Shelters",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetEvents_Locations_LocationId",
                table: "PetEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Locations_LocationId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_Shelters_Locations_LocationId",
                table: "Shelters");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Shelters_LocationId",
                table: "Shelters");

            migrationBuilder.DropIndex(
                name: "IX_Pets_LocationId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_PetEvents_LocationId",
                table: "PetEvents");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Shelters");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "PetEvents");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Shelters",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Pets",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: string.Empty);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "PetEvents",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
