using System.Collections.Generic;
using System.Linq;

namespace DigitalFamilyCookbook.Data.Models
{
    public class RoleType
    {
        public int RoleTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public IEnumerable<UserAccountRoleType> UserAccountRoleTypes { get; set; } = Enumerable.Empty<UserAccountRoleType>();

        public Recipe Recipe { get; set; } = Recipe.None();

        public static RoleType None() => new RoleType();
    }
}