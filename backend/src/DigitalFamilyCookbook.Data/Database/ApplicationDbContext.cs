#nullable disable

using DigitalFamilyCookbook.Data.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.Data.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Meat> Meats { get; set; }

    public DbSet<Recipe> Recipes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Step> Steps { get; set; }

    public DbSet<RoleType> RoleTypes { get; set; }

    public DbSet<RecipeCategory> RecipeCategories { get; set; }

    public DbSet<RecipeMeat> RecipeMeats { get; set; }

    public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

    public DbSet<RecipeStep> RecipeSteps { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<UserAccount> UserAccounts { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region "application.UserAccount"

        modelBuilder.Entity<UserAccount>()
            .ToTable("UserAccount", schema: "application");

        modelBuilder.Entity<UserAccount>()
            .Property(u => u.Name)
            .HasMaxLength(255)
            .IsRequired();

        #endregion

        #region "application.RefreshToken"

        modelBuilder.Entity<RefreshToken>()
            .ToTable("RefreshToken", schema: "application");

        modelBuilder.Entity<RefreshToken>()
            .HasKey(rt => rt.RefreshTokenId)
            .HasName("PK_Application_RefreshToken_RefreshTokenId");

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.Token)
            .HasMaxLength(500)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.JwtId)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.IsUsed)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.IsRevoked)
            .IsRequired();

        modelBuilder.Entity<RefreshToken>()
            .Property(rt => rt.AddedDate)
            .IsRequired()
            .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.UserAccount)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserAccountId);

        #endregion

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

        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Name)
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
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeIngredient>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Step"

        modelBuilder.Entity<Step>()
            .ToTable("Step", schema: "recipe");

        modelBuilder.Entity<Step>()
            .HasKey(s => s.StepId)
            .HasName("PK_Recipe_Step_StepId");

        modelBuilder.Entity<Step>()
            .Property(s => s.Direction)
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();

        #endregion

        #region "recipe.RecipeStep"

        modelBuilder.Entity<RecipeStep>()
            .ToTable("RecipeStep", schema: "recipe");

        modelBuilder.Entity<RecipeStep>()
            .HasKey(rs => rs.RecipeStepId)
            .HasName("PK_Recipe_RecipeStep_RecipeStepId");

        modelBuilder.Entity<RecipeStep>()
            .HasIndex(rs => new { rs.RecipeId, rs.StepId })
            .HasDatabaseName("UQ_Recipe_RecipeStep_RecipeId_StepId")
            .IsUnique();

        modelBuilder.Entity<RecipeStep>()
            .HasOne(rs => rs.Recipe)
            .WithMany(r => r.RecipeSteps)
            .HasForeignKey(rs => rs.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeStep>()
            .HasOne(rs => rs.Step)
            .WithMany(s => s.RecipeSteps)
            .HasForeignKey(rs => rs.StepId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Category"

        modelBuilder.Entity<Category>()
            .ToTable("Category", schema: "recipe");

        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryId)
            .HasName("PK_Recipe_Category_CategoryId");

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        #endregion

        #region "recipe.RecipeCategory"

        modelBuilder.Entity<RecipeCategory>()
            .ToTable("RecipeCategory", schema: "recipe");

        modelBuilder.Entity<RecipeCategory>()
            .HasKey(rc => rc.RecipeCategoryId)
            .HasName("PK_Recipe_RecipeCategory_RecipeCategoryId");

        modelBuilder.Entity<RecipeCategory>()
            .HasIndex(rc => new { rc.RecipeId, rc.CategoryId })
            .HasDatabaseName("UQ_Recipe_RecipeCategory_RecipeId_CategoryId")
            .IsUnique();

        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.Recipe)
            .WithMany(r => r.RecipeCategories)
            .HasForeignKey(rc => rc.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeCategory>()
            .HasOne(rc => rc.Category)
            .WithMany(c => c.RecipeCategories)
            .HasForeignKey(rc => rc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Meat"

        modelBuilder.Entity<Meat>()
            .ToTable("Meat", schema: "recipe");

        modelBuilder.Entity<Meat>()
            .HasKey(m => m.MeatId)
            .HasName("PK_Recipe_Meat_MeatyId");

        modelBuilder.Entity<Meat>()
            .Property(m => m.Name)
            .HasMaxLength(50)
            .IsRequired();

        #endregion

        #region "recipe.RecipeMeat"

        modelBuilder.Entity<RecipeMeat>()
            .ToTable("RecipeMeat", schema: "recipe");

        modelBuilder.Entity<RecipeMeat>()
            .HasKey(rm => rm.RecipeMeatId)
            .HasName("PK_Recipe_RecipeMeat_RecipeMeatId");

        modelBuilder.Entity<RecipeMeat>()
            .HasIndex(rm => new { rm.RecipeId, rm.MeatId })
            .HasDatabaseName("UQ_Recipe_RecipeMeat_RecipeId_MeatId")
            .IsUnique();

        modelBuilder.Entity<RecipeMeat>()
            .HasOne(rm => rm.Recipe)
            .WithMany(r => r.RecipeMeats)
            .HasForeignKey(rm => rm.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeMeat>()
            .HasOne(rm => rm.Meat)
            .WithMany(m => m.RecipeMeats)
            .HasForeignKey(rm => rm.MeatId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "application.RoleType"

        modelBuilder.Entity<RoleType>()
            .ToTable("RoleType", schema: "application");

        modelBuilder.Entity<RoleType>()
            .Property(rt => rt.Name)
            .HasMaxLength(25)
            .IsRequired();

        #endregion
    }
}