using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CustomerFriendshipsIgnored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Customers_CustomerId",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_CustomerId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Friendships");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Friendships",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_CustomerId",
                table: "Friendships",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Customers_CustomerId",
                table: "Friendships",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
