using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "ExpenseId1",
                table: "ExpenseMultipliers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Accounts",
                type: "character varying(128)",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ExpenseItemId",
                table: "ExpenseParticipantItems",
                column: "ExpenseItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ExpenseParticipantId1",
                table: "ExpenseParticipantItems",
                column: "ExpenseParticipantId1");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseMultipliers_ExpenseId1",
                table: "ExpenseMultipliers",
                column: "ExpenseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_StatusId",
                table: "Accounts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountStatus_StatusId",
                table: "Accounts",
                column: "StatusId",
                principalTable: "AccountStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseMultipliers_Expenses_ExpenseId1",
                table: "ExpenseMultipliers",
                column: "ExpenseId1",
                principalTable: "Expenses",
                principalColumn: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountStatus_StatusId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseMultipliers_Expenses_ExpenseId1",
                table: "ExpenseMultipliers");

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

            migrationBuilder.DropIndex(
                name: "IX_ExpenseMultipliers_ExpenseId1",
                table: "ExpenseMultipliers");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_StatusId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ExpenseItemId",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropColumn(
                name: "ExpenseParticipantId1",
                table: "ExpenseParticipantItems");

            migrationBuilder.DropColumn(
                name: "ExpenseId1",
                table: "ExpenseMultipliers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "ExternalId",
                table: "Accounts",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128,
                oldNullable: true);
        }
    }
}
