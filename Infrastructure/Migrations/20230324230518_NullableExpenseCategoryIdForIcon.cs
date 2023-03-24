﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NullableExpenseCategoryIdForIcon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Icons_CustomExpenseCategories_ExpenseCategoryId",
                table: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Icons_ExpenseCategoryId",
                table: "Icons");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExpenseCategoryId",
                table: "Icons",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Icons_ExpenseCategoryId",
                table: "Icons",
                column: "ExpenseCategoryId",
                unique: true,
                filter: "[ExpenseCategoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_CustomExpenseCategories_ExpenseCategoryId",
                table: "Icons",
                column: "ExpenseCategoryId",
                principalTable: "CustomExpenseCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Icons_CustomExpenseCategories_ExpenseCategoryId",
                table: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Icons_ExpenseCategoryId",
                table: "Icons");

            migrationBuilder.AlterColumn<Guid>(
                name: "ExpenseCategoryId",
                table: "Icons",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Icons_ExpenseCategoryId",
                table: "Icons",
                column: "ExpenseCategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Icons_CustomExpenseCategories_ExpenseCategoryId",
                table: "Icons",
                column: "ExpenseCategoryId",
                principalTable: "CustomExpenseCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
