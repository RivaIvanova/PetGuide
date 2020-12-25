namespace PetGuide.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPicturePropertyToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Pictures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_PostId",
                table: "Pictures",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Posts_PostId",
                table: "Pictures",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Posts_PostId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_PostId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Pictures");
        }
    }
}
