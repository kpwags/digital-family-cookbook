#nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.Data.Database;

public class ApplicationDbContext : IdentityDbContext<UserAccountDto, RoleTypeDto, string, UserAccountClaimDto, UserAccountRoleTypeDto, UserAccountLoginDto, RoleTypeClaimDto, UserAccountTokenDto>
{
    public DbSet<CategoryDto> Categories { get; set; }

    public DbSet<IngredientDto> Ingredients { get; set; }

    public DbSet<MeatDto> Meats { get; set; }

    public DbSet<RecipeDto> Recipes { get; set; }

    public DbSet<RecipeCategoryDto> RecipeCategories { get; set; }

    public DbSet<RecipeIngredientDto> RecipeIngredients { get; set; }

    public DbSet<RecipeMeatDto> RecipeMeats { get; set; }

    public DbSet<RecipeStepDto> RecipeSteps { get; set; }

    public DbSet<RoleTypeClaimDto> RoleTypeClaims { get; set; }

    public DbSet<RoleTypeDto> RoleTypes { get; set; }

    public DbSet<SiteSettingsDto> SiteSettings { get; set; }

    public DbSet<StepDto> Steps { get; set; }

    public DbSet<UserAccountDto> UserAccounts { get; set; }

    public DbSet<UserAccountClaimDto> UserAccountClaims { get; set; }

    public DbSet<UserAccountLoginDto> UserAccountLogins { get; set; }

    public DbSet<UserAccountRoleTypeDto> UserAccountRoleTypes { get; set; }

    public DbSet<UserAccountTokenDto> UserAccountTokens { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region "application.RoleType"

        modelBuilder.Entity<RoleTypeDto>()
            .ToTable("RoleType", schema: "application");

        #endregion

        #region "application.RoleTypeClaim"

        modelBuilder.Entity<RoleTypeClaimDto>()
            .ToTable("RoleTypeClaim", schema: "application");

        #endregion

        #region "application.SiteSettings"

        modelBuilder.Entity<SiteSettingsDto>()
            .ToTable("SiteSettings", schema: "application");

        modelBuilder.Entity<SiteSettingsDto>()
            .HasKey(s => s.SiteSettingsId)
            .HasName("PK_Application_SiteSettings_SiteSettingsId");

        modelBuilder.Entity<SiteSettingsDto>()
            .Property(s => s.Title)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<SiteSettingsDto>()
            .Property(s => s.IsPublic)
            .IsRequired()
            .HasDefaultValue(false);

        modelBuilder.Entity<SiteSettingsDto>()
            .Property(s => s.AllowPublicRegistration)
            .IsRequired()
            .HasDefaultValue(false);

        modelBuilder.Entity<SiteSettingsDto>()
            .Property(s => s.InvitationCode)
            .IsRequired()
            .HasDefaultValue(Guid.NewGuid().ToString());

        modelBuilder.Entity<SiteSettingsDto>()
            .Property(s => s.SaveRecipesOnDeleteUser)
            .IsRequired()
            .HasDefaultValue(true);

        #endregion

        #region "application.UserAccount"

        modelBuilder.Entity<UserAccountDto>()
            .ToTable("UserAccount", schema: "application");

        modelBuilder.Entity<UserAccountDto>()
            .Property(u => u.Name)
            .HasMaxLength(255)
            .IsRequired();
        #endregion

        #region "application.UserAccountClaim"

        modelBuilder.Entity<UserAccountClaimDto>()
            .ToTable("UserAccountClaim", schema: "application");

        #endregion

        #region "application.UserAccountLogin"

        modelBuilder.Entity<UserAccountLoginDto>()
            .ToTable("UserAccountLogin", schema: "application");

        #endregion

        #region "application.UserAccountRoleType"

        modelBuilder.Entity<UserAccountRoleTypeDto>()
            .ToTable("UserAccountRoleType", schema: "application");

        #endregion

        #region "application.UserAccountToken"

        modelBuilder.Entity<UserAccountTokenDto>()
            .ToTable("UserAccountToken", schema: "application");

        #endregion

        #region "recipe.Recipe"

        modelBuilder.Entity<RecipeDto>()
            .ToTable("Recipe", schema: "recipe");

        modelBuilder.Entity<RecipeDto>()
            .HasKey(r => r.RecipeId)
            .HasName("PK_Recipe_Recipe_RecipeId");

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Name)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Description)
            .HasMaxLength(2000);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.IsPublic)
            .HasDefaultValue(false);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Notes)
            .HasMaxLength(2000);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Source)
            .HasMaxLength(255);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.SourceUrl)
            .HasMaxLength(500);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.ImageUrl)
            .HasMaxLength(500);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.ImageUrlLarge)
            .HasMaxLength(500);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Calories)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Carbohydrates)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Sugar)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Fat)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Protein)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Fiber)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Cholesterol)
            .HasPrecision(10, 2);

        modelBuilder.Entity<RecipeDto>()
            .HasOne(r => r.UserAccount)
            .WithMany(u => u.Recipes);

        #endregion

        #region "recipe.Ingredient"

        modelBuilder.Entity<IngredientDto>()
            .ToTable("Ingredient", schema: "recipe");

        modelBuilder.Entity<IngredientDto>()
            .HasKey(i => i.IngredientId)
            .HasName("PK_Recipe_Ingredient_IngredientId");

        modelBuilder.Entity<IngredientDto>()
            .Property(i => i.Name)
            .HasMaxLength(255)
            .IsRequired();

        #endregion

        #region "recipe.RecipeIngredient"

        modelBuilder.Entity<RecipeIngredientDto>()
            .ToTable("RecipeIngredient", schema: "recipe");

        modelBuilder.Entity<RecipeIngredientDto>()
            .HasKey(ri => ri.RecipeIngredientId)
            .HasName("PK_Recipe_RecipeIngredient_RecipeIngredientId");

        modelBuilder.Entity<RecipeIngredientDto>()
            .HasIndex(ri => new { ri.RecipeId, ri.IngredientId })
            .HasDatabaseName("UQ_Recipe_RecipeIngredient_RecipeId_IngredientId")
            .IsUnique();

        modelBuilder.Entity<RecipeIngredientDto>()
            .HasOne(ri => ri.Recipe)
            .WithMany(r => r.RecipeIngredients)
            .HasForeignKey(ri => ri.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeIngredientDto>()
            .HasOne(ri => ri.Ingredient)
            .WithMany(i => i.RecipeIngredients)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Step"

        modelBuilder.Entity<StepDto>()
            .ToTable("Step", schema: "recipe");

        modelBuilder.Entity<StepDto>()
            .HasKey(s => s.StepId)
            .HasName("PK_Recipe_Step_StepId");

        modelBuilder.Entity<StepDto>()
            .Property(s => s.Direction)
            .HasColumnType("VARCHAR(MAX)")
            .IsRequired();

        #endregion

        #region "recipe.RecipeStep"

        modelBuilder.Entity<RecipeStepDto>()
            .ToTable("RecipeStep", schema: "recipe");

        modelBuilder.Entity<RecipeStepDto>()
            .HasKey(rs => rs.RecipeStepId)
            .HasName("PK_Recipe_RecipeStep_RecipeStepId");

        modelBuilder.Entity<RecipeStepDto>()
            .HasIndex(rs => new { rs.RecipeId, rs.StepId })
            .HasDatabaseName("UQ_Recipe_RecipeStep_RecipeId_StepId")
            .IsUnique();

        modelBuilder.Entity<RecipeStepDto>()
            .HasOne(rs => rs.Recipe)
            .WithMany(r => r.RecipeSteps)
            .HasForeignKey(rs => rs.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeStepDto>()
            .HasOne(rs => rs.Step)
            .WithMany(s => s.RecipeSteps)
            .HasForeignKey(rs => rs.StepId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Category"

        modelBuilder.Entity<CategoryDto>()
            .ToTable("Category", schema: "recipe");

        modelBuilder.Entity<CategoryDto>()
            .HasKey(c => c.CategoryId)
            .HasName("PK_Recipe_Category_CategoryId");

        modelBuilder.Entity<CategoryDto>()
            .Property(c => c.Name)
            .HasMaxLength(50)
            .IsRequired();

        #endregion

        #region "recipe.RecipeCategory"

        modelBuilder.Entity<RecipeCategoryDto>()
            .ToTable("RecipeCategory", schema: "recipe");

        modelBuilder.Entity<RecipeCategoryDto>()
            .HasKey(rc => rc.RecipeCategoryId)
            .HasName("PK_Recipe_RecipeCategory_RecipeCategoryId");

        modelBuilder.Entity<RecipeCategoryDto>()
            .HasIndex(rc => new { rc.RecipeId, rc.CategoryId })
            .HasDatabaseName("UQ_Recipe_RecipeCategory_RecipeId_CategoryId")
            .IsUnique();

        modelBuilder.Entity<RecipeCategoryDto>()
            .HasOne(rc => rc.Recipe)
            .WithMany(r => r.RecipeCategories)
            .HasForeignKey(rc => rc.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeCategoryDto>()
            .HasOne(rc => rc.Category)
            .WithMany(c => c.RecipeCategories)
            .HasForeignKey(rc => rc.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region "recipe.Meat"

        modelBuilder.Entity<MeatDto>()
            .ToTable("Meat", schema: "recipe");

        modelBuilder.Entity<MeatDto>()
            .HasKey(m => m.MeatId)
            .HasName("PK_Recipe_Meat_MeatyId");

        modelBuilder.Entity<MeatDto>()
            .Property(m => m.Name)
            .HasMaxLength(50)
            .IsRequired();

        #endregion

        #region "recipe.RecipeMeat"

        modelBuilder.Entity<RecipeMeatDto>()
            .ToTable("RecipeMeat", schema: "recipe");

        modelBuilder.Entity<RecipeMeatDto>()
            .HasKey(rm => rm.RecipeMeatId)
            .HasName("PK_Recipe_RecipeMeat_RecipeMeatId");

        modelBuilder.Entity<RecipeMeatDto>()
            .HasIndex(rm => new { rm.RecipeId, rm.MeatId })
            .HasDatabaseName("UQ_Recipe_RecipeMeat_RecipeId_MeatId")
            .IsUnique();

        modelBuilder.Entity<RecipeMeatDto>()
            .HasOne(rm => rm.Recipe)
            .WithMany(r => r.RecipeMeats)
            .HasForeignKey(rm => rm.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeMeatDto>()
            .HasOne(rm => rm.Meat)
            .WithMany(m => m.RecipeMeats)
            .HasForeignKey(rm => rm.MeatId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
    }
}