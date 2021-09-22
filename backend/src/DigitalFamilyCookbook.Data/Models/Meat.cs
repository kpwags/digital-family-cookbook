using System.Collections.Generic;
using System.Linq;

namespace DigitalFamilyCookbook.Data.Models
{
    public class Meat
    {
        public int MeatId { get; set; }

        public string Name { get; set; } = string.Empty;

        public Recipe Recipe { get; set; } = Recipe.None();

        public IEnumerable<RecipeMeat> RecipeMeats { get; set; } = Enumerable.Empty<RecipeMeat>();

        public static Meat None() => new Meat();
    }
}