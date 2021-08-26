using Microsoft.EntityFrameworkCore.Migrations;

namespace TestRent.Migrations
{
    public partial class NotUniqueIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_ClientId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RentedBookId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ClientId",
                table: "Transactions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RentedBookId",
                table: "Transactions",
                column: "RentedBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_ClientId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RentedBookId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ClientId",
                table: "Transactions",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RentedBookId",
                table: "Transactions",
                column: "RentedBookId",
                unique: true);
        }
    }
}
