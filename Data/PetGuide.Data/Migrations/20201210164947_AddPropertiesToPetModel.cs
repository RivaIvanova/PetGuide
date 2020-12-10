namespace PetGuide.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPropertiesToPetModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pets");
        }
    }
}
