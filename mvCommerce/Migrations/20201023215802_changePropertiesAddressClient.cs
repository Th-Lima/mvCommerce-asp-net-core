using Microsoft.EntityFrameworkCore.Migrations;

namespace mvCommerce.Migrations
{
    public partial class changePropertiesAddressClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetNumber",
                table: "Client",
                newName: "Complement");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "Client",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Client",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Client",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complement",
                table: "Client",
                newName: "StreetNumber");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Client",
                newName: "Street");

            migrationBuilder.AlterColumn<string>(
                name: "Sex",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Client",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
