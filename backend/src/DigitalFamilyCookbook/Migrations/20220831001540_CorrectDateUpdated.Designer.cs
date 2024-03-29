﻿// <auto-generated />
using System;
using DigitalFamilyCookbook.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalFamilyCookbook.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220831001540_CorrectDateUpdated")]
    partial class CorrectDateUpdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.CategoryDto", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId")
                        .HasName("PK_Recipe_Category_CategoryId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_Category_Name");

                    b.ToTable("Category", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.IngredientDto", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int?>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("IngredientId")
                        .HasName("PK_Recipe_Ingredient_IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredient", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.MeatDto", b =>
                {
                    b.Property<int>("MeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MeatId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MeatId")
                        .HasName("PK_Recipe_Meat_MeatyId");

                    b.ToTable("Meat", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.NoteDto", b =>
                {
                    b.Property<int>("NoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("NoteText")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("nvarchar(4000)");

                    b.HasKey("NoteId")
                        .HasName("PK_Application_Note_NoteId");

                    b.ToTable("Note", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeCategoryDto", b =>
                {
                    b.Property<int>("RecipeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeCategoryId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("RecipeCategoryId")
                        .HasName("PK_Recipe_RecipeCategory_RecipeCategoryId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RecipeId", "CategoryId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeCategory_RecipeId_CategoryId");

                    b.ToTable("RecipeCategory", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeDto", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeId"), 1L, 1);

                    b.Property<int?>("ActiveTime")
                        .HasColumnType("int");

                    b.Property<decimal?>("Calories")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("Carbohydrates")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("Cholesterol")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<decimal?>("Fat")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("Fiber")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrlLarge")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Protein")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Servings")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SourceUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal?>("Sugar")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int?>("Time")
                        .HasColumnType("int");

                    b.Property<string>("UserAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeId")
                        .HasName("PK_Recipe_Recipe_RecipeId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_Recipe_Name");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Recipe", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeFavoriteDto", b =>
                {
                    b.Property<int>("RecipeFavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeFavoriteId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("UserAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RecipeFavoriteId")
                        .HasName("PK_Recipe_RecipeFavorite");

                    b.HasIndex("UserAccountId");

                    b.HasIndex("RecipeId", "UserAccountId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeFavorite_RecipeId_UserAccountId");

                    b.ToTable("RecipeFavorite", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeMeatDto", b =>
                {
                    b.Property<int>("RecipeMeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeMeatId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("MeatId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("RecipeMeatId")
                        .HasName("PK_Recipe_RecipeMeat_RecipeMeatId");

                    b.HasIndex("MeatId");

                    b.HasIndex("RecipeId", "MeatId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeMeat_RecipeId_MeatId");

                    b.ToTable("RecipeMeat", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeNoteDto", b =>
                {
                    b.Property<int>("RecipeNoteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RecipeNoteId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("NoteId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("RecipeNoteId")
                        .HasName("PK_Recipe_RecipeNote_RecipeNoteId");

                    b.HasIndex("NoteId");

                    b.HasIndex("RecipeId", "NoteId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeNote_RecipeId_NoteId");

                    b.ToTable("RecipeNote", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RefreshTokenDto", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefreshTokenId"), 1L, 1);

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RefreshTokenId")
                        .HasName("PK_Application_RefreshToken_RefreshTokenId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("RefreshToken", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RoleTypeClaimDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleClaimId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleTypeClaim", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RoleTypeDto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RoleTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("RoleType", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.SiteSettingsDto", b =>
                {
                    b.Property<int>("SiteSettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiteSettingsId"), 1L, 1);

                    b.Property<bool>("AllowPublicRegistration")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvitationCode")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("80bd1c38-433e-4649-b09e-1b29d8dac8d2");

                    b.Property<bool>("IsPublic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool?>("SaveRecipesOnDeleteUser")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SiteSettingsId")
                        .HasName("PK_Application_SiteSettings_SiteSettingsId");

                    b.ToTable("SiteSettings", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.StepDto", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StepId"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("StepId")
                        .HasName("PK_Recipe_Step_StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Step", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountClaimDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAccountClaimId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccountClaim", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("UserAccount", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountLoginDto", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAccountLoginId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccountLogin", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountRoleTypeDto", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserAccountRoleTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserAccountRoleType", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountTokenDto", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserAccountTokenId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserAccountToken", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.IngredientDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeCategoryDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.CategoryDto", "Category")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", "UserAccount")
                        .WithMany("Recipes")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeFavoriteDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("RecipeFavorites")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", "UserAccount")
                        .WithMany("RecipeFavorites")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeMeatDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.MeatDto", "Meat")
                        .WithMany("RecipeMeats")
                        .HasForeignKey("MeatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("RecipeMeats")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Meat");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeNoteDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.NoteDto", "Note")
                        .WithMany("RecipeNotes")
                        .HasForeignKey("NoteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("RecipeNotes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RefreshTokenDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", "UserAccount")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RoleTypeClaimDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RoleTypeDto", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.StepDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RecipeDto", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountClaimDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountLoginDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountRoleTypeDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.RoleTypeDto", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountTokenDto", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.CategoryDto", b =>
                {
                    b.Navigation("RecipeCategories");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.MeatDto", b =>
                {
                    b.Navigation("RecipeMeats");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.NoteDto", b =>
                {
                    b.Navigation("RecipeNotes");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.RecipeDto", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("RecipeCategories");

                    b.Navigation("RecipeFavorites");

                    b.Navigation("RecipeMeats");

                    b.Navigation("RecipeNotes");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Dtos.UserAccountDto", b =>
                {
                    b.Navigation("RecipeFavorites");

                    b.Navigation("Recipes");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
