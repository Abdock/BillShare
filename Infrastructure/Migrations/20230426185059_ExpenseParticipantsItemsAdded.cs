using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseParticipantsItemsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseParticipantItems_ExpenseItems_ExpenseItemId",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseParticipantItems_ExpenseParticipants_ExpensePartici~1",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseParticipantItems_ExpenseItemId",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseParticipantItems_ExpenseParticipantId1",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropColumn(
                name: "ExpenseItemId",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropColumn(
                name: "ExpenseParticipantId1",
                table: "ExpenseParticipantItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseItemId",
                table: "ExpenseParticipantItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseParticipantId1",
                table: "ExpenseParticipantItems",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ExpenseItemId",
                table: "ExpenseParticipantItems",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ExpenseParticipantId1",
                table: "ExpenseParticipantItems",
                column: "ExpenseParticipantId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseParticipantItems_ExpenseItems_ExpenseItemId",
                table: "ExpenseParticipantItems",
                column: "ExpenseItemId",
                principalTable: "ExpenseItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseParticipantItems_ExpenseParticipants_ExpensePartici~1",
                table: "ExpenseParticipantItems",
                column: "ExpenseParticipantId1",
                principalTable: "ExpenseParticipants",
                principalColumn: "Id");
        }
    }
}
