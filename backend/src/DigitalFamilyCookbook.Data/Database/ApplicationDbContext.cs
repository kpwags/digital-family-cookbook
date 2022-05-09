#nullable disable

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalFamilyCookbook.Data.Database;

public class ApplicationDbContext : IdentityDbContext<UserAccountDto, RoleTypeDto, string, UserAccountClaimDto, UserAccountRoleTypeDto, UserAccountLoginDto, RoleTypeClaimDto, UserAccountTokenDto>
{
    public DbSet<CategoryDto> Categories { get; set; }

    public DbSet<IngredientDto> Ingredients { get; set; }

    public DbSet<MeatDto> Meats { get; set; }

    public DbSet<NoteDto> Notes { get; set; }

    public DbSet<RecipeDto> Recipes { get; set; }

    public DbSet<RecipeCategoryDto> RecipeCategories { get; set; }

    public DbSet<RecipeMeatDto> RecipeMeats { get; set; }

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

        #region "application.Note"

        modelBuilder.Entity<NoteDto>()
            .ToTable("Note", schema: "application");

        modelBuilder.Entity<NoteDto>()
            .HasKey(n => n.NoteId)
            .HasName("PK_Application_Note_NoteId");

        modelBuilder.Entity<NoteDto>()
            .Property(n => n.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<NoteDto>()
            .Property(n => n.NoteText)
            .HasMaxLength(4000)
            .IsRequired();

        modelBuilder.Entity<NoteDto>()
            .Property(n => n.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<NoteDto>()
            .Property(n => n.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

        #endregion

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
            .Property(r => r.Servings)
            .HasDefaultValue(1);

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

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeDto>()
            .HasMany(r => r.Ingredients)
            .WithOne(i => i.Recipe);

        modelBuilder.Entity<RecipeDto>()
            .HasMany(r => r.Steps)
            .WithOne(s => s.Recipe);

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeDto>()
            .Property(r => r.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<IngredientDto>()
            .Property(i => i.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<IngredientDto>()
            .HasOne(i => i.Recipe)
            .WithMany(r => r.Ingredients);

        modelBuilder.Entity<IngredientDto>()
            .Property(i => i.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<IngredientDto>()
            .Property(i => i.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<StepDto>()
            .Property(s => s.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<StepDto>()
            .HasOne(s => s.Recipe)
            .WithMany(r => r.Steps);

        modelBuilder.Entity<StepDto>()
            .Property(s => s.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<StepDto>()
            .Property(s => s.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<CategoryDto>()
            .HasIndex(c => c.Name)
            .HasDatabaseName("UQ_Recipe_Category_Name")
            .IsUnique();

        modelBuilder.Entity<CategoryDto>()
            .HasMany(c => c.RecipeCategories)
            .WithOne(rc => rc.Category);

        modelBuilder.Entity<CategoryDto>()
            .Property(c => c.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<CategoryDto>()
            .Property(c => c.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<CategoryDto>()
            .Property(c => c.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<RecipeCategoryDto>()
            .Property(rc => rc.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeCategoryDto>()
            .Property(rc => rc.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeCategoryDto>()
            .Property(rc => rc.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<MeatDto>()
            .Property(m => m.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<MeatDto>()
            .HasMany(m => m.RecipeMeats)
            .WithOne(rm => rm.Meat);

        modelBuilder.Entity<MeatDto>()
            .Property(m => m.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<MeatDto>()
            .Property(m => m.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

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

        modelBuilder.Entity<RecipeMeatDto>()
            .Property(rm => rm.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeMeatDto>()
            .Property(rm => rm.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeMeatDto>()
            .Property(rm => rm.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

        #endregion

        #region "recipe.RecipeNote"

        modelBuilder.Entity<RecipeNoteDto>()
            .ToTable("RecipeNote", schema: "recipe");

        modelBuilder.Entity<RecipeNoteDto>()
            .HasKey(rn => rn.RecipeNoteId)
            .HasName("PK_Recipe_RecipeNote_RecipeNoteId");

        modelBuilder.Entity<RecipeNoteDto>()
            .HasIndex(rm => new { rm.RecipeId, rm.NoteId })
            .HasDatabaseName("UQ_Recipe_RecipeNote_RecipeId_NoteId")
            .IsUnique();

        modelBuilder.Entity<RecipeNoteDto>()
            .HasOne(rn => rn.Recipe)
            .WithMany(r => r.RecipeNotes)
            .HasForeignKey(rn => rn.RecipeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeNoteDto>()
            .HasOne(rn => rn.Note)
            .WithMany(n => n.RecipeNotes)
            .HasForeignKey(rn => rn.NoteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RecipeNoteDto>()
            .Property(rn => rn.Id)
            .HasMaxLength(36)
            .HasDefaultValue(Guid.NewGuid().ToString())
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeNoteDto>()
            .Property(rn => rn.DateCreated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<RecipeNoteDto>()
            .Property(rn => rn.DateUpdated)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAddOrUpdate();

        #endregion
    }
}