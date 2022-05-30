using Microsoft.EntityFrameworkCore.Migrations;

namespace MVC.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "City", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Stockholm", "Johan Svensson", "+46725180302" },
                    { 2, "Göteborg", "Nils Kristiansson", "+46737470353" },
                    { 3, "Malmö", "Christoffer Nilsson", "+46736395900" },
                    { 4, "Helsingfors", "Pekka Heino", "+46725180305" },
                    { 5, "Köpenhamn", "Peter Rohde", "+46733080322" },
                    { 6, "Berlin", "Lisa Braun", "+46718180309" },
                    { 7, "Paris", "Blanche Berthelot", "+46739470303" },
                    { 8, "Madrid", "Diego Garcia", "+46739165309" },
                    { 9, "Stockholm", "Per Persson", "+46739145209" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
