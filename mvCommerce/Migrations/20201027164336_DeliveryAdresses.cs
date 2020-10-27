using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mvCommerce.Migrations
{
    public partial class DeliveryAdresses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryAdresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Neighborhood = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Complement = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: false),
                    Zipcode = table.Column<string>(maxLength: 10, nullable: false),
                    ClientId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAdresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAdresses_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAdresses_ClientId",
                table: "DeliveryAdresses",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAdresses");
        }
    }
}
