using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerAndTransactionMgt.Data.Migrations
{
    public partial class ChangedLedgerCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LegerCode",
                table: "Accounts",
                newName: "legerCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "legerCode",
                table: "Accounts",
                newName: "LegerCode");
        }
    }
}
