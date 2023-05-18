CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "AccountStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_AccountStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "ExpenseItemStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ExpenseItemStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "ExpenseParticipantItemStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ExpenseParticipantItemStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "ExpenseParticipantStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ExpenseParticipantStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "ExpenseStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ExpenseStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "ExpenseTypes" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_ExpenseTypes" PRIMARY KEY ("Id")
);

CREATE TABLE "FriendshipStatus" (
    "Id" integer NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "PK_FriendshipStatus" PRIMARY KEY ("Id")
);

CREATE TABLE "Passwords" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "Salt" character varying(64) NOT NULL,
    "EncryptedPassword" character varying(512) NOT NULL,
    CONSTRAINT "PK_Passwords" PRIMARY KEY ("Id")
);

CREATE TABLE "Roles" (
    "Id" integer NOT NULL,
    "Name" character varying(128) NOT NULL,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Id")
);

CREATE TABLE "Customers" (
    "Id" uuid NOT NULL,
    "ExternalId" character varying(128) NULL,
    "Email" character varying(128) NOT NULL,
    "TelegramHandle" character varying(128) NULL,
    "PhoneNumber" character varying(32) NULL,
    "Name" character varying(128) NOT NULL,
    "AvatarUrl" character varying(2048) NULL,
    "PasswordId" uuid NOT NULL,
    "RoleId" integer NOT NULL,
    CONSTRAINT "PK_Customers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Customers_Passwords_PasswordId" FOREIGN KEY ("PasswordId") REFERENCES "Passwords" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Customers_Roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Roles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Accounts" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "ExternalId" character varying(128) NOT NULL,
    "Amount" numeric(12,2) NOT NULL,
    "Name" character varying(128) NOT NULL,
    CONSTRAINT "PK_Accounts" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Accounts_Customers_UserId" FOREIGN KEY ("UserId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CustomExpenseCategories" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "IconId" uuid NOT NULL,
    CONSTRAINT "PK_CustomExpenseCategories" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_CustomExpenseCategories_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Friendships" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "FriendId" uuid NOT NULL,
    "StatusId" integer NOT NULL,
    "CustomerId" uuid NULL,
    CONSTRAINT "PK_Friendships" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Friendships_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id"),
    CONSTRAINT "FK_Friendships_Customers_FriendId" FOREIGN KEY ("FriendId") REFERENCES "Customers" ("Id"),
    CONSTRAINT "FK_Friendships_Customers_UserId" FOREIGN KEY ("UserId") REFERENCES "Customers" ("Id"),
    CONSTRAINT "FK_Friendships_FriendshipStatus_StatusId" FOREIGN KEY ("StatusId") REFERENCES "FriendshipStatus" ("Id") ON DELETE CASCADE
);

CREATE TABLE "RefreshTokens" (
    "Id" uuid NOT NULL,
    "Token" character varying(512) NOT NULL,
    "OwnerId" uuid NOT NULL,
    "ExpirationDateTime" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_RefreshTokens_Customers_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Expenses" (
    "Id" uuid NOT NULL,
    "CreatorId" uuid NOT NULL,
    "ExpenseTypeId" integer NOT NULL,
    "CategoryId" uuid NOT NULL,
    "AccountId" uuid NOT NULL,
    "Amount" numeric(12,2) NOT NULL,
    "DateTime" timestamp with time zone NOT NULL,
    "StatusId" integer NOT NULL,
    CONSTRAINT "PK_Expenses" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Expenses_Accounts_AccountId" FOREIGN KEY ("AccountId") REFERENCES "Accounts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Expenses_CustomExpenseCategories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES "CustomExpenseCategories" ("Id"),
    CONSTRAINT "FK_Expenses_Customers_CreatorId" FOREIGN KEY ("CreatorId") REFERENCES "Customers" ("Id"),
    CONSTRAINT "FK_Expenses_ExpenseStatus_StatusId" FOREIGN KEY ("StatusId") REFERENCES "ExpenseStatus" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Expenses_ExpenseTypes_ExpenseTypeId" FOREIGN KEY ("ExpenseTypeId") REFERENCES "ExpenseTypes" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Icons" (
    "Id" uuid NOT NULL,
    "Url" character varying(2048) NOT NULL,
    "ExpenseCategoryId" uuid NULL,
    CONSTRAINT "PK_Icons" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Icons_CustomExpenseCategories_ExpenseCategoryId" FOREIGN KEY ("ExpenseCategoryId") REFERENCES "CustomExpenseCategories" ("Id")
);

CREATE TABLE "ExpenseItems" (
    "Id" uuid NOT NULL,
    "ExpenseId" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Count" integer NOT NULL,
    "Amount" numeric(12,2) NOT NULL,
    "StatusId" integer NOT NULL,
    CONSTRAINT "PK_ExpenseItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ExpenseItems_ExpenseItemStatus_StatusId" FOREIGN KEY ("StatusId") REFERENCES "ExpenseItemStatus" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExpenseItems_Expenses_ExpenseId" FOREIGN KEY ("ExpenseId") REFERENCES "Expenses" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ExpenseMultipliers" (
    "Id" uuid NOT NULL,
    "ExpenseId" uuid NOT NULL,
    "Multiplier" numeric(5,2) NOT NULL,
    "Name" character varying(128) NOT NULL,
    CONSTRAINT "PK_ExpenseMultipliers" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ExpenseMultipliers_Expenses_ExpenseId" FOREIGN KEY ("ExpenseId") REFERENCES "Expenses" ("Id") ON DELETE CASCADE
);

CREATE TABLE "ExpenseParticipants" (
    "Id" uuid NOT NULL,
    "CustomerId" uuid NOT NULL,
    "ExpenseId" uuid NOT NULL,
    "StatusId" integer NOT NULL,
    CONSTRAINT "PK_ExpenseParticipants" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ExpenseParticipants_Customers_CustomerId" FOREIGN KEY ("CustomerId") REFERENCES "Customers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExpenseParticipants_ExpenseParticipantStatus_StatusId" FOREIGN KEY ("StatusId") REFERENCES "ExpenseParticipantStatus" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExpenseParticipants_Expenses_ExpenseId" FOREIGN KEY ("ExpenseId") REFERENCES "Expenses" ("Id")
);

CREATE TABLE "ExpenseParticipantItems" (
    "Id" uuid NOT NULL,
    "ExpenseParticipantId" uuid NOT NULL,
    "ItemId" uuid NOT NULL,
    "StatusId" integer NOT NULL,
    CONSTRAINT "PK_ExpenseParticipantItems" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_ExpenseParticipantItems_ExpenseItems_ItemId" FOREIGN KEY ("ItemId") REFERENCES "ExpenseItems" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExpenseParticipantItems_ExpenseParticipantItemStatus_Status~" FOREIGN KEY ("StatusId") REFERENCES "ExpenseParticipantItemStatus" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_ExpenseParticipantItems_ExpenseParticipants_ExpenseParticip~" FOREIGN KEY ("ExpenseParticipantId") REFERENCES "ExpenseParticipants" ("Id")
);

INSERT INTO "AccountStatus" ("Id", "Name")
VALUES (0, 'Active');
INSERT INTO "AccountStatus" ("Id", "Name")
VALUES (1, 'NotActive');

INSERT INTO "ExpenseItemStatus" ("Id", "Name")
VALUES (0, 'Active');
INSERT INTO "ExpenseItemStatus" ("Id", "Name")
VALUES (1, 'NotActive');

INSERT INTO "ExpenseParticipantItemStatus" ("Id", "Name")
VALUES (0, 'Selected');
INSERT INTO "ExpenseParticipantItemStatus" ("Id", "Name")
VALUES (1, 'Unselected');

INSERT INTO "ExpenseParticipantStatus" ("Id", "Name")
VALUES (0, 'Paid');
INSERT INTO "ExpenseParticipantStatus" ("Id", "Name")
VALUES (1, 'Pending');
INSERT INTO "ExpenseParticipantStatus" ("Id", "Name")
VALUES (2, 'Unpaid');

INSERT INTO "ExpenseStatus" ("Id", "Name")
VALUES (0, 'Active');
INSERT INTO "ExpenseStatus" ("Id", "Name")
VALUES (1, 'Finished');

INSERT INTO "ExpenseTypes" ("Id", "Name")
VALUES (0, 'Necessary');
INSERT INTO "ExpenseTypes" ("Id", "Name")
VALUES (1, 'Unexpected');
INSERT INTO "ExpenseTypes" ("Id", "Name")
VALUES (2, 'SelfExpenses');

INSERT INTO "FriendshipStatus" ("Id", "Name")
VALUES (0, 'Pending');
INSERT INTO "FriendshipStatus" ("Id", "Name")
VALUES (1, 'Accepted');
INSERT INTO "FriendshipStatus" ("Id", "Name")
VALUES (2, 'Rejected');

INSERT INTO "Roles" ("Id", "Name")
VALUES (0, 'User');
INSERT INTO "Roles" ("Id", "Name")
VALUES (1, 'Admin');

CREATE INDEX "IX_Accounts_UserId" ON "Accounts" ("UserId");

CREATE UNIQUE INDEX "IX_Customers_PasswordId" ON "Customers" ("PasswordId");

CREATE INDEX "IX_Customers_RoleId" ON "Customers" ("RoleId");

CREATE INDEX "IX_CustomExpenseCategories_CustomerId" ON "CustomExpenseCategories" ("CustomerId");

CREATE INDEX "IX_ExpenseItems_ExpenseId" ON "ExpenseItems" ("ExpenseId");

CREATE INDEX "IX_ExpenseItems_StatusId" ON "ExpenseItems" ("StatusId");

CREATE INDEX "IX_ExpenseMultipliers_ExpenseId" ON "ExpenseMultipliers" ("ExpenseId");

CREATE INDEX "IX_ExpenseParticipantItems_ExpenseParticipantId" ON "ExpenseParticipantItems" ("ExpenseParticipantId");

CREATE INDEX "IX_ExpenseParticipantItems_ItemId" ON "ExpenseParticipantItems" ("ItemId");

CREATE INDEX "IX_ExpenseParticipantItems_StatusId" ON "ExpenseParticipantItems" ("StatusId");

CREATE INDEX "IX_ExpenseParticipants_CustomerId" ON "ExpenseParticipants" ("CustomerId");

CREATE INDEX "IX_ExpenseParticipants_ExpenseId" ON "ExpenseParticipants" ("ExpenseId");

CREATE INDEX "IX_ExpenseParticipants_StatusId" ON "ExpenseParticipants" ("StatusId");

CREATE INDEX "IX_Expenses_AccountId" ON "Expenses" ("AccountId");

CREATE INDEX "IX_Expenses_CategoryId" ON "Expenses" ("CategoryId");

CREATE INDEX "IX_Expenses_CreatorId" ON "Expenses" ("CreatorId");

CREATE INDEX "IX_Expenses_ExpenseTypeId" ON "Expenses" ("ExpenseTypeId");

CREATE INDEX "IX_Expenses_StatusId" ON "Expenses" ("StatusId");

CREATE INDEX "IX_Friendships_CustomerId" ON "Friendships" ("CustomerId");

CREATE INDEX "IX_Friendships_FriendId" ON "Friendships" ("FriendId");

CREATE INDEX "IX_Friendships_StatusId" ON "Friendships" ("StatusId");

CREATE INDEX "IX_Friendships_UserId" ON "Friendships" ("UserId");

CREATE UNIQUE INDEX "IX_Icons_ExpenseCategoryId" ON "Icons" ("ExpenseCategoryId");

CREATE INDEX "IX_RefreshTokens_OwnerId" ON "RefreshTokens" ("OwnerId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230326201226_InitMigration', '7.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "ExpenseParticipantItems" ADD "ExpenseItemId" uuid NULL;

ALTER TABLE "ExpenseParticipantItems" ADD "ExpenseParticipantId1" uuid NULL;

ALTER TABLE "ExpenseMultipliers" ADD "ExpenseId1" uuid NULL;

ALTER TABLE "Accounts" ALTER COLUMN "ExternalId" DROP NOT NULL;

ALTER TABLE "Accounts" ADD "StatusId" integer NOT NULL DEFAULT 0;

CREATE INDEX "IX_ExpenseParticipantItems_ExpenseItemId" ON "ExpenseParticipantItems" ("ExpenseItemId");

CREATE INDEX "IX_ExpenseParticipantItems_ExpenseParticipantId1" ON "ExpenseParticipantItems" ("ExpenseParticipantId1");

CREATE INDEX "IX_ExpenseMultipliers_ExpenseId1" ON "ExpenseMultipliers" ("ExpenseId1");

CREATE INDEX "IX_Accounts_StatusId" ON "Accounts" ("StatusId");

ALTER TABLE "Accounts" ADD CONSTRAINT "FK_Accounts_AccountStatus_StatusId" FOREIGN KEY ("StatusId") REFERENCES "AccountStatus" ("Id") ON DELETE CASCADE;

ALTER TABLE "ExpenseMultipliers" ADD CONSTRAINT "FK_ExpenseMultipliers_Expenses_ExpenseId1" FOREIGN KEY ("ExpenseId1") REFERENCES "Expenses" ("Id");

ALTER TABLE "ExpenseParticipantItems" ADD CONSTRAINT "FK_ExpenseParticipantItems_ExpenseItems_ExpenseItemId" FOREIGN KEY ("ExpenseItemId") REFERENCES "ExpenseItems" ("Id");

ALTER TABLE "ExpenseParticipantItems" ADD CONSTRAINT "FK_ExpenseParticipantItems_ExpenseParticipants_ExpensePartici~1" FOREIGN KEY ("ExpenseParticipantId1") REFERENCES "ExpenseParticipants" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230329213511_AddStatusToAccount', '7.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "Friendships" DROP CONSTRAINT "FK_Friendships_Customers_CustomerId";

DROP INDEX "IX_Friendships_CustomerId";

ALTER TABLE "Friendships" DROP COLUMN "CustomerId";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230417155214_CustomerFriendshipsIgnored', '7.0.4');

COMMIT;

START TRANSACTION;

CREATE TABLE "Groups" (
    "Id" uuid NOT NULL,
    "GroupName" character varying(128) NOT NULL,
    "CreatorId" uuid NOT NULL,
    CONSTRAINT "PK_Groups" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Groups_Customers_CreatorId" FOREIGN KEY ("CreatorId") REFERENCES "Customers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CustomerGroup" (
    "GroupsId" uuid NOT NULL,
    "ParticipantsId" uuid NOT NULL,
    CONSTRAINT "PK_CustomerGroup" PRIMARY KEY ("GroupsId", "ParticipantsId"),
    CONSTRAINT "FK_CustomerGroup_Customers_ParticipantsId" FOREIGN KEY ("ParticipantsId") REFERENCES "Customers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CustomerGroup_Groups_GroupsId" FOREIGN KEY ("GroupsId") REFERENCES "Groups" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_CustomerGroup_ParticipantsId" ON "CustomerGroup" ("ParticipantsId");

CREATE INDEX "IX_Groups_CreatorId" ON "Groups" ("CreatorId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230418193226_AddGroups', '7.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "Expenses" ADD "Name" character varying(128) NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230425111618_AddNameForExpense', '7.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "ExpenseParticipantItems" DROP CONSTRAINT "FK_ExpenseParticipantItems_ExpenseItems_ExpenseItemId";

ALTER TABLE "ExpenseParticipantItems" DROP CONSTRAINT "FK_ExpenseParticipantItems_ExpenseParticipants_ExpensePartici~1";

DROP INDEX "IX_ExpenseParticipantItems_ExpenseItemId";

DROP INDEX "IX_ExpenseParticipantItems_ExpenseParticipantId1";

ALTER TABLE "ExpenseParticipantItems" DROP COLUMN "ExpenseItemId";

ALTER TABLE "ExpenseParticipantItems" DROP COLUMN "ExpenseParticipantId1";

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20230426185059_ExpenseParticipantsItemsAdded', '7.0.4');

COMMIT;

