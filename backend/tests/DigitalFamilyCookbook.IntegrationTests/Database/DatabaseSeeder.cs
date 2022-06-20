using DigitalFamilyCookbook.Data.Database;

namespace DigitalFamilyCookbook.IntegrationTests.Database;

public static class DatabaseSeeder
{
    private static ApplicationDbContext _db = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>());

    public static async Task Seed(ApplicationDbContext db)
    {
        _db = db;

        await SeedSiteSettings();
        await SeedRoles();
        await SeedUsers();
        await SeedCategories();
        await SeedMeats();
        await SeedRecipes();
    }

    public static async Task SeedSiteSettings()
    {
        _db.SiteSettings.Add(new SiteSettingsDto
        {
            AllowPublicRegistration = false,
            Id = MockDataGenerator.RandomId(),
            InvitationCode = MockDataGenerator.RandomId(),
            IsPublic = false,
            SaveRecipesOnDeleteUser = true,
            SiteSettingsId = 1,
            Title = "Digital Family Cookbook",
        });

        await _db.SaveChangesAsync();
    }

    public static async Task SeedRoles()
    {
        _db.Roles.Add(new RoleTypeDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "USERROLEID",
            Name = "User",
            NormalizedName = "USER",
            RoleTypeId = MockDataGenerator.RandomId(),
        });

        _db.Roles.Add(new RoleTypeDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "ADMINROLEID",
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            RoleTypeId = MockDataGenerator.RandomId(),
        });

        await _db.SaveChangesAsync();
    }

    public static async Task SeedUsers()
    {
        var userRole = _db.Roles.FirstOrDefault(r => r.NormalizedName == "USER");
        var adminRole = _db.Roles.FirstOrDefault(r => r.NormalizedName == "ADMINISTRATOR");

        if (userRole is null || adminRole is null)
        {
            return;
        }

        _db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "JAMESTKIRK1701",
            UserName = "james.t.kirk@starfleet.gov",
            NormalizedUserName = "JAMES.T.KIRK@STARFLEET.GOV",
            Email = "james.t.kirk@starfleet.gov",
            NormalizedEmail = "JAMES.T.KIRK@STARFLEET.GOV",
            UserId = "JAMESTKIRK1701",
        });

        await _db.SaveChangesAsync();

        var kirk = _db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "JAMES.T.KIRK@STARFLEET.GOV");

        if (kirk is not null)
        {
            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = kirk.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = adminRole.Id,
                UserId = kirk.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            await _db.SaveChangesAsync();
        }

        _db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "JEANLUCPICARD1701D",
            UserName = "jeanluc.picard@starfleet.gov",
            NormalizedUserName = "JEANLUC.PICARD@STARFLEET.GOV",
            Email = "jeanluc.picard@starfleet.gov",
            NormalizedEmail = "JEANLUC.PICARD@STARFLEET.GOV",
            UserId = "JEANLUCPICARD1701D",
        });

        await _db.SaveChangesAsync();

        var picard = _db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "JEANLUC.PICARD@STARFLEET.GOV");

        if (picard is not null)
        {
            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = picard.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = adminRole.Id,
                UserId = picard.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            await _db.SaveChangesAsync();
        }

        _db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "BENJAMINSISKO74205",
            UserName = "benjamin.sisko@starfleet.gov",
            NormalizedUserName = "BENJAMIN.SISKO@STARFLEET.GOV",
            Email = "benjamin.sisko@starfleet.gov",
            NormalizedEmail = "BENJAMIN.SISKO@STARFLEET.GOV",
            UserId = "BENJAMINSISKO74205",
            Name = "Ben Sisko",
        });

        await _db.SaveChangesAsync();

        var sisko = _db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "BENJAMIN.SISKO@STARFLEET.GOV");

        if (sisko is not null)
        {
            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = sisko.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });
            await _db.SaveChangesAsync();
        }

        _db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = "KATHRYNJANEWAY74656",
            UserName = "kathryn.janeway@starfleet.gov",
            NormalizedUserName = "KATHRYN.JANEWAY@STARFLEET.GOV",
            Email = "kathryn.janeway@starfleet.gov",
            NormalizedEmail = "KATHRYN.JANEWAY@STARFLEET.GOV",
            UserId = "KATHRYNJANEWAY74656",
        });

        await _db.SaveChangesAsync();

        var janeway = _db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "KATHRYN.JANEWAY@STARFLEET.GOV");

        if (janeway is not null)
        {
            _db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = janeway.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            await _db.SaveChangesAsync();
        }
    }

    public static async Task SeedCategories()
    {
        _db.Categories.Add(new CategoryDto
        {
            CategoryId = 1,
            Id = MockDataGenerator.RandomId(),
            Name = "Italian",
        });

        _db.Categories.Add(new CategoryDto
        {
            CategoryId = 2,
            Id = MockDataGenerator.RandomId(),
            Name = "Grilled",
        });

        _db.Categories.Add(new CategoryDto
        {
            CategoryId = 3,
            Id = MockDataGenerator.RandomId(),
            Name = "Mexican",
        });

        _db.Categories.Add(new CategoryDto
        {
            CategoryId = 4,
            Id = MockDataGenerator.RandomId(),
            Name = "Slow Cooker",
        });

        _db.Categories.Add(new CategoryDto
        {
            CategoryId = 5,
            Id = MockDataGenerator.RandomId(),
            Name = "Asian",
        });

        await _db.SaveChangesAsync();
    }

    public static async Task SeedMeats()
    {
        _db.Meats.Add(new MeatDto
        {
            MeatId = 1,
            Id = MockDataGenerator.RandomId(),
            Name = "Beef",
        });

        _db.Meats.Add(new MeatDto
        {
            MeatId = 2,
            Id = MockDataGenerator.RandomId(),
            Name = "Chicken",
        });

        _db.Meats.Add(new MeatDto
        {
            MeatId = 3,
            Id = MockDataGenerator.RandomId(),
            Name = "Pork",
        });

        _db.Meats.Add(new MeatDto
        {
            MeatId = 4,
            Id = MockDataGenerator.RandomId(),
            Name = "Fish",
        });

        _db.Meats.Add(new MeatDto
        {
            MeatId = 5,
            Id = MockDataGenerator.RandomId(),
            Name = "Vegetarian",
        });

        await _db.SaveChangesAsync();
    }

    public static async Task SeedRecipes()
    {
        _db.Recipes.Add(new RecipeDto
        {
            RecipeId = 1,
            Id = MockDataGenerator.RandomId(),
            Name = "Lime Baked Tilapia",
            Description = "<p>This is an easy to make, high-protein meal</p>",
            Servings = 2,
            Source = "recipes.com",
            SourceUrl = "https://recipes.com/recipes-lime-baked-tilapia",
            Time = 20,
            ActiveTime = 8,
            Calories = 300,
            Protein = 23,
            Carbohydrates = 3,
            Fat = 6,
            Sugar = 1,
            Cholesterol = 56,
            Fiber = 0,
            UserAccountId = "JAMESTKIRK1701",
            UserAccount = _db.UserAccounts.First(u => u.Id == "JAMESTKIRK1701"),
            RecipeMeats = new List<RecipeMeatDto>
            {
                new RecipeMeatDto
                {
                    Id = MockDataGenerator.RandomId(),
                    MeatId = 4,
                    Meat = _db.Meats.First(m => m.MeatId == 4),
                },
            },
            Ingredients = new List<IngredientDto>
            {
                new IngredientDto
                {
                    IngredientId = 1,
                    Id = MockDataGenerator.RandomId(),
                    Name = "2 Tilapia Filets",
                    SortOrder = 1,
                },

                new IngredientDto
                {
                    IngredientId = 2,
                    Id = MockDataGenerator.RandomId(),
                    Name = "2 tsp. Butter",
                    SortOrder = 2,
                },

                new IngredientDto
                {
                    IngredientId = 3,
                    Id = MockDataGenerator.RandomId(),
                    Name = "1 Lime",
                    SortOrder = 3,
                },

                new IngredientDto
                {
                    IngredientId = 4,
                    Id = MockDataGenerator.RandomId(),
                    Name = "Salt & Pepper",
                    SortOrder = 4,
                },
            },
            Steps = new List<StepDto>
            {
                new StepDto
                {
                    StepId = 1,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Pre-heat oven to 375.",
                    SortOrder = 1,
                },

                new StepDto
                {
                    StepId = 2,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Place tilapia filets on baking sheet and sprinkle with salt & pepper.",
                    SortOrder = 2,
                },

                new StepDto
                {
                    StepId = 3,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Place 1 tsp. butter on each filet. Zest and juice lemon over filets.",
                    SortOrder = 3,
                },

                new StepDto
                {
                    StepId = 4,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Bake in oven for 8-12 minutes.",
                    SortOrder = 4,
                },
            },
        });

        _db.Recipes.Add(new RecipeDto
        {
            RecipeId = 2,
            Id = MockDataGenerator.RandomId(),
            Name = "Peanut Butter & Jelly",
            Description = "<p>An all-time classic</p>",
            Servings = 1,
            Time = 5,
            ActiveTime = 5,
            Calories = 500,
            Protein = 12,
            Carbohydrates = 23,
            Fat = 16,
            Sugar = 10,
            Cholesterol = 76,
            Fiber = 12,
            UserAccountId = "BENJAMINSISKO74205",
            UserAccount = _db.UserAccounts.First(u => u.Id == "BENJAMINSISKO74205"),
            RecipeMeats = new List<RecipeMeatDto>
            {
                new RecipeMeatDto
                {
                    Id = MockDataGenerator.RandomId(),
                    MeatId = 5,
                    Meat = _db.Meats.First(m => m.MeatId == 5),
                },
            },
            Ingredients = new List<IngredientDto>
            {
                new IngredientDto
                {
                    IngredientId = 5,
                    Id = MockDataGenerator.RandomId(),
                    Name = "2 tbsp. Peanut Butter",
                    SortOrder = 1,
                },

                new IngredientDto
                {
                    IngredientId = 6,
                    Id = MockDataGenerator.RandomId(),
                    Name = "2 tbsp. Jelly",
                    SortOrder = 2,
                },

                new IngredientDto
                {
                    IngredientId = 7,
                    Id = MockDataGenerator.RandomId(),
                    Name = "2 Slices Bread",
                    SortOrder = 3,
                },
            },
            Steps = new List<StepDto>
            {
                new StepDto
                {
                    StepId = 5,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Use knife to spread peanut butter on once slice of bread. Then spread jelly on other slice.",
                    SortOrder = 1,
                },

                new StepDto
                {
                    StepId = 6,
                    Id = MockDataGenerator.RandomId(),
                    Direction = "Place both slices of bread together so that the PB and Jelly touch",
                    SortOrder = 2,
                },
            },
        });

        await _db.SaveChangesAsync();
    }
}
