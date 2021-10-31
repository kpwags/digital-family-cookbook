﻿// <auto-generated />
using System;
using DigitalFamilyCookbook.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DigitalFamilyCookbook.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("CategoryId")
                        .HasName("PK_Recipe_Category_CategoryId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Category", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Meat", b =>
                {
                    b.Property<int>("MeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("MeatId")
                        .HasName("PK_Recipe_Meat_MeatyId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Meat", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Recipe", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<decimal?>("Fat")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal?>("Fiber")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrlLarge")
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

                    b.Property<string>("Notes")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

                    b.Property<decimal?>("Protein")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Source")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("SourceUrl")
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

                    b.HasIndex("UserAccountId");

                    b.ToTable("Recipe", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeCategory", b =>
                {
                    b.Property<int>("RecipeCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeIngredient", b =>
                {
                    b.Property<int>("RecipeIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.HasKey("RecipeIngredientId")
                        .HasName("PK_Recipe_RecipeIngredient_RecipeIngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId", "IngredientId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeIngredient_RecipeId_IngredientId");

                    b.ToTable("RecipeIngredient", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeMeat", b =>
                {
                    b.Property<int>("RecipeMeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeStep", b =>
                {
                    b.Property<int>("RecipeStepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.HasKey("RecipeStepId")
                        .HasName("PK_Recipe_RecipeStep_RecipeStepId");

                    b.HasIndex("StepId");

                    b.HasIndex("RecipeId", "StepId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Recipe_RecipeStep_RecipeId_StepId");

                    b.ToTable("RecipeStep", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RoleType", b =>
                {
                    b.Property<int>("RoleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("UserAccountId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoleTypeId")
                        .HasName("PK_Application_RoleType_RoleTypeId");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserAccountId");

                    b.ToTable("RoleType", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Step", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direction")
                        .IsRequired()
                        .HasColumnType("VARCHAR(MAX)");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.HasKey("StepId")
                        .HasName("PK_Recipe_Step_StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("Step", "recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.UserAccount", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAccount", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.UserAccountRoleType", b =>
                {
                    b.Property<int>("UserAccountRoleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserAccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserAccountRoleTypeId")
                        .HasName("PK_Application_UserAccountRoleType_UserAccountRoleTypeId");

                    b.HasIndex("RoleTypeId");

                    b.HasIndex("UserAccountId", "RoleTypeId")
                        .IsUnique()
                        .HasDatabaseName("UQ_Application_UserAccountRoleType_UserAccountId_RoleTypeId");

                    b.ToTable("UserAccountRoleType", "application");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Category", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("Categories")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Ingredient", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Meat", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("Meats")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Recipe", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.UserAccount", "UserAccount")
                        .WithMany("Recipes")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeCategory", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Category", "Category")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeCategories")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeIngredient", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Ingredient", "Ingredient")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeIngredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeMeat", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Meat", "Meat")
                        .WithMany("RecipeMeats")
                        .HasForeignKey("MeatId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeMeats")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Meat");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RecipeStep", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Models.Step", "Step")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RoleType", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany()
                        .HasForeignKey("RecipeId");

                    b.HasOne("DigitalFamilyCookbook.Data.Models.UserAccount", null)
                        .WithMany("RoleTypes")
                        .HasForeignKey("UserAccountId");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Step", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.Recipe", "Recipe")
                        .WithMany("Steps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.UserAccountRoleType", b =>
                {
                    b.HasOne("DigitalFamilyCookbook.Data.Models.RoleType", "RoleType")
                        .WithMany("UserAccountRoleTypes")
                        .HasForeignKey("RoleTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DigitalFamilyCookbook.Data.Models.UserAccount", "UserAccount")
                        .WithMany("UserAccountRoleTypes")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RoleType");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Category", b =>
                {
                    b.Navigation("RecipeCategories");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Ingredient", b =>
                {
                    b.Navigation("RecipeIngredients");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Meat", b =>
                {
                    b.Navigation("RecipeMeats");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Recipe", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Ingredients");

                    b.Navigation("Meats");

                    b.Navigation("RecipeCategories");

                    b.Navigation("RecipeIngredients");

                    b.Navigation("RecipeMeats");

                    b.Navigation("RecipeSteps");

                    b.Navigation("Steps");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.RoleType", b =>
                {
                    b.Navigation("UserAccountRoleTypes");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.Step", b =>
                {
                    b.Navigation("RecipeSteps");
                });

            modelBuilder.Entity("DigitalFamilyCookbook.Data.Models.UserAccount", b =>
                {
                    b.Navigation("Recipes");

                    b.Navigation("RoleTypes");

                    b.Navigation("UserAccountRoleTypes");
                });
#pragma warning restore 612, 618
        }
    }
}
