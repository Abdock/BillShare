using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseParticipantItemStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseParticipantItemStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseParticipantStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseParticipantStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendshipStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Salt = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EncryptedPassword = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Email = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TelegramHandle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AvatarUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    PasswordId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Passwords_PasswordId",
                        column: x => x.PasswordId,
                        principalTable: "Passwords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExternalId = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomExpenseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    IconId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomExpenseCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomExpenseCategories_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FriendId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Friendships_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friendships_Customers_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friendships_Customers_UserId",
                        column: x => x.UserId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Friendships_FriendshipStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "FriendshipStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpirationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Customers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseTypeId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_CustomExpenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CustomExpenseCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_Customers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ExpenseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseTypes_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ExpenseCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Icons_CustomExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "CustomExpenseCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_ExpenseItemStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ExpenseItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseMultipliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Multiplier = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseMultipliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseMultipliers_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseParticipants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseParticipants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipants_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipants_ExpenseParticipantStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ExpenseParticipantStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipants_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExpenseParticipantItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpenseParticipantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseParticipantItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipantItems_ExpenseItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "ExpenseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipantItems_ExpenseParticipantItemStatus_Status~",
                        column: x => x.StatusId,
                        principalTable: "ExpenseParticipantItemStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseParticipantItems_ExpenseParticipants_ExpenseParticip~",
                        column: x => x.ExpenseParticipantId,
                        principalTable: "ExpenseParticipants",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AccountStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Active" },
                    { 1, "NotActive" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseItemStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Active" },
                    { 1, "NotActive" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseParticipantItemStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Selected" },
                    { 1, "Unselected" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseParticipantStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Paid" },
                    { 1, "Pending" },
                    { 2, "Unpaid" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Active" },
                    { 1, "Finished" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Necessary" },
                    { 1, "Unexpected" },
                    { 2, "SelfExpenses" }
                });

            migrationBuilder.InsertData(
                table: "FriendshipStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "Pending" },
                    { 1, "Accepted" },
                    { 2, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 0, "User" },
                    { 1, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PasswordId",
                table: "Customers",
                column: "PasswordId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RoleId",
                table: "Customers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomExpenseCategories_CustomerId",
                table: "CustomExpenseCategories",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_ExpenseId",
                table: "ExpenseItems",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_StatusId",
                table: "ExpenseItems",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseMultipliers_ExpenseId",
                table: "ExpenseMultipliers",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ExpenseParticipantId",
                table: "ExpenseParticipantItems",
                column: "ExpenseParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_ItemId",
                table: "ExpenseParticipantItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipantItems_StatusId",
                table: "ExpenseParticipantItems",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipants_CustomerId",
                table: "ExpenseParticipants",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipants_ExpenseId",
                table: "ExpenseParticipants",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseParticipants_StatusId",
                table: "ExpenseParticipants",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_AccountId",
                table: "Expenses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CategoryId",
                table: "Expenses",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatorId",
                table: "Expenses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseTypeId",
                table: "Expenses",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_StatusId",
                table: "Expenses",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_CustomerId",
                table: "Friendships",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_FriendId",
                table: "Friendships",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_StatusId",
                table: "Friendships",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserId",
                table: "Friendships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Icons_ExpenseCategoryId",
                table: "Icons",
                column: "ExpenseCategoryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_OwnerId",
                table: "RefreshTokens",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatus");

            migrationBuilder.DropTable(
                name: "ExpenseMultipliers");

            migrationBuilder.DropTable(
                name: "ExpenseParticipantItems");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ExpenseItems");

            migrationBuilder.DropTable(
                name: "ExpenseParticipantItemStatus");

            migrationBuilder.DropTable(
                name: "ExpenseParticipants");

            migrationBuilder.DropTable(
                name: "FriendshipStatus");

            migrationBuilder.DropTable(
                name: "ExpenseItemStatus");

            migrationBuilder.DropTable(
                name: "ExpenseParticipantStatus");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "CustomExpenseCategories");

            migrationBuilder.DropTable(
                name: "ExpenseStatus");

            migrationBuilder.DropTable(
                name: "ExpenseTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
