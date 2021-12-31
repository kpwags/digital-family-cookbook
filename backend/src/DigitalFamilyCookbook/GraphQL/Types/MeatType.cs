using DigitalFamilyCookbook.Data.Models;
using GraphQL.Types;

namespace DigitalFamilyCookbook.GraphQL.Types
{
    public class MeatType : ObjectGraphType<Meat>
    {
        public MeatType()
        {
            Name = "Meat";

            Field(m => m.Id, type: typeof(IdGraphType)).Description("The ID of the Meat");
            Field(m => m.MeatId, type: typeof(IntGraphType)).Description("The SQL ID of the Meat");
            Field(m => m.Name, type: typeof(StringGraphType)).Description("The Name of the Meat");
        }
    }
}