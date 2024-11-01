#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManagement.InventoryManagement.Infrastructure.Persistence;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Products",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: false),
                Price = table.Column<decimal>("decimal(18,2)", nullable: false),
                Stock = table.Column<int>("int", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Products", x => x.Id); });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Products");
    }
}