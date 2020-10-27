using Microsoft.EntityFrameworkCore.Migrations;

namespace mvCommerce.Migrations
{
    public partial class NameOfDeliveryAddressIsNotRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryAdresses",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "DeliveryAdresses",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
