using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogRep.Migrations
{
    public partial class IntialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Recipes",
                newName: "Protein");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "Recipes",
                newName: "Description");
        }
    }
}
