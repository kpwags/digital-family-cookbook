using DigitalFamilyCookbook.Data.Database;

namespace DigitalFamilyCookbook.IntegrationTests;

public static class TestPreparation
{
    public static void Seed(ApplicationDbContext db)
    {
        SeedSiteSettings(db);
        SeedRoles(db);
        SeedUsers(db);
    }

    public static void SeedSiteSettings(ApplicationDbContext db)
    {
        db.SiteSettings.Add(new SiteSettingsDto
        {
            AllowPublicRegistration = false,
            Id = MockDataGenerator.RandomId(),
            InvitationCode = MockDataGenerator.RandomId(),
            IsPublic = false,
            SaveRecipesOnDeleteUser = true,
            SiteSettingsId = 1,
            Title = "Digital Family Cookbook",
        });
    }

    public static void SeedRoles(ApplicationDbContext db)
    {
        db.Roles.Add(new RoleTypeDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            Name = "User",
            NormalizedName = "USER",
            RoleTypeId = MockDataGenerator.RandomId(),
        });

        db.Roles.Add(new RoleTypeDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            RoleTypeId = MockDataGenerator.RandomId(),
        });
    }

    public static void SeedUsers(ApplicationDbContext db)
    {
        var userRole = db.Roles.FirstOrDefault(r => r.NormalizedName == "USER");
        var adminRole = db.Roles.FirstOrDefault(r => r.NormalizedName == "ADMINISTRATOR");

        if (userRole is null || adminRole is null)
        {
            return;
        }

        db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            UserName = "james.t.kirk@starfleet.gov",
            NormalizedUserName = "JAMES.T.KIRK@STARFLEET.GOV",
            Email = "james.t.kirk@starfleet.gov",
            NormalizedEmail = "JAMES.T.KIRK@STARFLEET.GOV",
            UserId = MockDataGenerator.RandomId(),
        });

        var kirk = db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "JAMES.T.KIRK@STARFLEET.GOV");

        if (kirk is not null)
        {
            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = kirk.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = adminRole.Id,
                UserId = kirk.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });
        }

        db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            UserName = "jeanluc.picard@starfleet.gov",
            NormalizedUserName = "JEANLUC.PICARD@STARFLEET.GOV",
            Email = "jeanluc.picard@starfleet.gov",
            NormalizedEmail = "JEANLUC.PICARD@STARFLEET.GOV",
            UserId = MockDataGenerator.RandomId(),
        });

        var picard = db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "JEANLUC.PICARD@STARFLEET.GOV");

        if (picard is not null)
        {
            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = picard.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });

            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = adminRole.Id,
                UserId = picard.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });
        }

        db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            UserName = "benjamin.sisko@starfleet.gov",
            NormalizedUserName = "BENJAMIN.SISKO@STARFLEET.GOV",
            Email = "benjamin.sisko@starfleet.gov",
            NormalizedEmail = "BENJAMIN.SISKO@STARFLEET.GOV",
            UserId = MockDataGenerator.RandomId(),
        });

        var sisko = db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "BENJAMIN.SISKO@STARFLEET.GOV");

        if (sisko is not null)
        {
            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = sisko.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });
        }

        db.UserAccounts.Add(new UserAccountDto
        {
            ConcurrencyStamp = MockDataGenerator.RandomId(),
            Id = MockDataGenerator.RandomId(),
            UserName = "kathryn.janeway@starfleet.gov",
            NormalizedUserName = "KATHRYN.JANEWAY@STARFLEET.GOV",
            Email = "kathryn.janeway@starfleet.gov",
            NormalizedEmail = "KATHRYN.JANEWAY@STARFLEET.GOV",
            UserId = MockDataGenerator.RandomId(),
        });

        var janeway = db.UserAccounts.FirstOrDefault(u => u.NormalizedEmail == "KATHRYN.JANEWAY@STARFLEET.GOV");

        if (janeway is not null)
        {
            db.UserAccountRoleTypes.Add(new UserAccountRoleTypeDto
            {
                RoleId = userRole.Id,
                UserId = janeway.Id,
                UserAccountRoleTypeId = MockDataGenerator.RandomId()
            });
        }
    }
}
