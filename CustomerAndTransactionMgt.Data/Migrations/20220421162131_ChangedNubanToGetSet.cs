using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAndTransactionMgt.Data.Migrations
{
    public partial class ChangedNubanToGetSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nuban",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nuban",
                table: "Accounts");
        }
    }
}
