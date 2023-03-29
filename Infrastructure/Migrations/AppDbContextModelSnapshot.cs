﻿// <auto-generated />
using System;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<string>("ExternalId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Domain.Models.AccountStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AccountStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 1,
                            Name = "NotActive"
                        });
                });

            modelBuilder.Entity("Domain.Models.CustomExpenseCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("IconId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomExpenseCategories");
                });

            modelBuilder.Entity("Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ExternalId")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<Guid>("PasswordId")
                        .HasColumnType("uuid");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(32)
                        .HasColumnType("character varying(32)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("TelegramHandle")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("PasswordId")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Models.Expense", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CreatorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ExpenseTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("ExpenseTypeId");

                    b.HasIndex("StatusId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Domain.Models.ExpenseItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("StatusId");

                    b.ToTable("ExpenseItems");
                });

            modelBuilder.Entity("Domain.Models.ExpenseItemStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseItemStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 1,
                            Name = "NotActive"
                        });
                });

            modelBuilder.Entity("Domain.Models.ExpenseMultiplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExpenseId1")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Multiplier")
                        .HasPrecision(5, 2)
                        .HasColumnType("numeric(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("ExpenseId1");

                    b.ToTable("ExpenseMultipliers");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExpenseId")
                        .HasColumnType("uuid");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ExpenseId");

                    b.HasIndex("StatusId");

                    b.ToTable("ExpenseParticipants");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipantItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExpenseItemId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExpenseParticipantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExpenseParticipantId1")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemId")
                        .HasColumnType("uuid");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseItemId");

                    b.HasIndex("ExpenseParticipantId");

                    b.HasIndex("ExpenseParticipantId1");

                    b.HasIndex("ItemId");

                    b.HasIndex("StatusId");

                    b.ToTable("ExpenseParticipantItems");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipantItemStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseParticipantItemStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Selected"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Unselected"
                        });
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipantStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseParticipantStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Paid"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Unpaid"
                        });
                });

            modelBuilder.Entity("Domain.Models.ExpenseStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Active"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Finished"
                        });
                });

            modelBuilder.Entity("Domain.Models.ExpenseType", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ExpenseTypes");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Necessary"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Unexpected"
                        },
                        new
                        {
                            Id = 2,
                            Name = "SelfExpenses"
                        });
                });

            modelBuilder.Entity("Domain.Models.Friendship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uuid");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("FriendId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Domain.Models.FriendshipStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FriendshipStatus");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "Pending"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Accepted"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Rejected"
                        });
                });

            modelBuilder.Entity("Domain.Models.Icon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ExpenseCategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("character varying(2048)");

                    b.HasKey("Id");

                    b.HasIndex("ExpenseCategoryId")
                        .IsUnique();

                    b.ToTable("Icons");
                });

            modelBuilder.Entity("Domain.Models.Password", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<string>("EncryptedPassword")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)");

                    b.HasKey("Id");

                    b.ToTable("Passwords");
                });

            modelBuilder.Entity("Domain.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpirationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("character varying(512)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 0,
                            Name = "User"
                        },
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Domain.Models.Account", b =>
                {
                    b.HasOne("Domain.Models.AccountStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.CustomExpenseCategory", b =>
                {
                    b.HasOne("Domain.Models.Customer", "Customer")
                        .WithMany("ExpenseCategories")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Domain.Models.Customer", b =>
                {
                    b.HasOne("Domain.Models.Password", "Password")
                        .WithOne("Customer")
                        .HasForeignKey("Domain.Models.Customer", "PasswordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Password");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Models.Expense", b =>
                {
                    b.HasOne("Domain.Models.Account", "Account")
                        .WithMany("Expenses")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.CustomExpenseCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.Customer", "Creator")
                        .WithMany("CreatedExpenses")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseType", "ExpenseType")
                        .WithMany()
                        .HasForeignKey("ExpenseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Category");

                    b.Navigation("Creator");

                    b.Navigation("ExpenseType");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.ExpenseItem", b =>
                {
                    b.HasOne("Domain.Models.Expense", "Expense")
                        .WithMany("ExpenseItems")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseItemStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Expense");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.ExpenseMultiplier", b =>
                {
                    b.HasOne("Domain.Models.Expense", "Expense")
                        .WithMany()
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Expense", null)
                        .WithMany("ExpenseMultipliers")
                        .HasForeignKey("ExpenseId1");

                    b.Navigation("Expense");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipant", b =>
                {
                    b.HasOne("Domain.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Expense", "Expense")
                        .WithMany("ExpenseParticipants")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseParticipantStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Expense");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipantItem", b =>
                {
                    b.HasOne("Domain.Models.ExpenseItem", null)
                        .WithMany("ExpenseParticipantItems")
                        .HasForeignKey("ExpenseItemId");

                    b.HasOne("Domain.Models.ExpenseParticipant", "Participant")
                        .WithMany()
                        .HasForeignKey("ExpenseParticipantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseParticipant", null)
                        .WithMany("ExpenseParticipantItems")
                        .HasForeignKey("ExpenseParticipantId1")
                        .HasConstraintName("FK_ExpenseParticipantItems_ExpenseParticipants_ExpensePartici~1");

                    b.HasOne("Domain.Models.ExpenseItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.ExpenseParticipantItemStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Participant");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Domain.Models.Friendship", b =>
                {
                    b.HasOne("Domain.Models.Customer", null)
                        .WithMany("Friendships")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Domain.Models.Customer", "Friend")
                        .WithMany("FriendFriendships")
                        .HasForeignKey("FriendId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Models.FriendshipStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Customer", "User")
                        .WithMany("UserFriendships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Friend");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Models.Icon", b =>
                {
                    b.HasOne("Domain.Models.CustomExpenseCategory", "ExpenseCategory")
                        .WithOne("Icon")
                        .HasForeignKey("Domain.Models.Icon", "ExpenseCategoryId");

                    b.Navigation("ExpenseCategory");
                });

            modelBuilder.Entity("Domain.Models.RefreshToken", b =>
                {
                    b.HasOne("Domain.Models.Customer", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.Models.Account", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("Domain.Models.CustomExpenseCategory", b =>
                {
                    b.Navigation("Icon")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Customer", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("CreatedExpenses");

                    b.Navigation("ExpenseCategories");

                    b.Navigation("FriendFriendships");

                    b.Navigation("Friendships");

                    b.Navigation("UserFriendships");
                });

            modelBuilder.Entity("Domain.Models.Expense", b =>
                {
                    b.Navigation("ExpenseItems");

                    b.Navigation("ExpenseMultipliers");

                    b.Navigation("ExpenseParticipants");
                });

            modelBuilder.Entity("Domain.Models.ExpenseItem", b =>
                {
                    b.Navigation("ExpenseParticipantItems");
                });

            modelBuilder.Entity("Domain.Models.ExpenseParticipant", b =>
                {
                    b.Navigation("ExpenseParticipantItems");
                });

            modelBuilder.Entity("Domain.Models.Password", b =>
                {
                    b.Navigation("Customer")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
