#nullable disable

using DigitalFamilyCookbook.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Meat> Meats { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<RoleType> RoleTypes { get; set; }

        public DbSet<UserAccountRoleType> UserAccountRoleTypes { get; set; }

        public DbSet<RecipeCategory> RecipeCategories { get; set; }

        public DbSet<RecipeMeat> RecipeMeats { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<RecipeStep> RecipeSteps { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region "recipe.Recipe"

            modelBuilder.Entity<Recipe>()
                .ToTable("Recipe", schema: "recipe");

            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.RecipeId)
                .HasName("PK_Recipe_Recipe_RecipeId");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Description)
                .HasMaxLength(2000);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.IsPublic)
                .HasDefaultValue(false);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Notes)
                .HasMaxLength(2000);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Source)
                .HasMaxLength(255);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.SourceUrl)
                .HasMaxLength(500);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.ImageUrl)
                .HasMaxLength(500);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.ImageUrlLarge)
                .HasMaxLength(500);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Calories)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Carbohydrates)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Sugar)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Fat)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Protein)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Fiber)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Cholesterol)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.UserAccount)
                .WithMany(u => u.Recipes);

            #endregion

            #region "recipe.Ingredient"

            modelBuilder.Entity<Ingredient>()
                .ToTable("Ingredient", schema: "recipe");

            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.IngredientId)
                .HasName("PK_Recipe_Ingredient_IngredientId");

            modelBuilder.Entity<Recipe>()
                .Property(r => r.Name)
                .HasMaxLength(255)
                .IsRequired();

            #endregion

            #region "recipe.RecipeIngredient"

            modelBuilder.Entity<RecipeIngredient>()
                .ToTable("RecipeIngredient", schema: "recipe");

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => ri.RecipeIngredientId)
                .HasName("PK_Recipe_RecipeIngredient_RecipeIngredientId");

            modelBuilder.Entity<RecipeIngredient>()
                .HasIndex(ri => new { ri.RecipeId, ri.IngredientId })
                .HasDatabaseName("UQ_Recipe_RecipeIngredient_RecipeId_IngredientId")
                .IsUnique();

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany(i => i.RecipeIngredients)
                .HasForeignKey(ri => ri.IngredientId);

            #endregion

            modelBuilder.Entity<RecipeStep>()
                .HasIndex(rs => new { rs.RecipeId, rs.StepId })
                .IsUnique();

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId);

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Step)
                .WithMany(s => s.RecipeSteps)
                .HasForeignKey(rs => rs.StepId);

            modelBuilder.Entity<RecipeCategory>()
                .HasIndex(rc => new { rc.RecipeId, rc.CategoryId })
                .IsUnique();

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Recipe)
                .WithMany(r => r.RecipeCategories)
                .HasForeignKey(rc => rc.RecipeId);

            modelBuilder.Entity<RecipeCategory>()
                .HasOne(rc => rc.Category)
                .WithMany(c => c.RecipeCategories)
                .HasForeignKey(rc => rc.CategoryId);

            modelBuilder.Entity<RecipeMeat>()
                .HasIndex(rm => new { rm.RecipeId, rm.MeatId })
                .IsUnique();

            modelBuilder.Entity<RecipeMeat>()
                .HasOne(rm => rm.Recipe)
                .WithMany(r => r.RecipeMeats)
                .HasForeignKey(rm => rm.RecipeId);

            modelBuilder.Entity<RecipeMeat>()
                .HasOne(rm => rm.Meat)
                .WithMany(m => m.RecipeMeats)
                .HasForeignKey(rm => rm.MeatId);

            modelBuilder.Entity<UserAccountRoleType>()
                .HasIndex(uart => new { uart.UserAccountId, uart.RoleTypeId })
                .IsUnique();

            modelBuilder.Entity<UserAccountRoleType>()
                .HasOne(uart => uart.UserAccount)
                .WithMany(u => u.UserAccountRoleTypes)
                .HasForeignKey(uart => uart.UserAccountId);

            modelBuilder.Entity<UserAccountRoleType>()
                .HasOne(uart => uart.RoleType)
                .WithMany(rt => rt.UserAccountRoleTypes)
                .HasForeignKey(uart => uart.RoleTypeId);
        }
    }
}