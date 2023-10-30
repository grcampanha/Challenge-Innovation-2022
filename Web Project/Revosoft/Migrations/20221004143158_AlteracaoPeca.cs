using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revosoft.Migrations
{
    public partial class AlteracaoPeca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BateriaScore",
                table: "Pecas",
                type: "decimal(10,2)",
                maxLength: 3,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BateriaScore",
                table: "Pecas");
        }
    }
}
