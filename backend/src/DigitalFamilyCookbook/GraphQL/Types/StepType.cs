namespace DigitalFamilyCookbook.GraphQL.Types;

public class StepType : ObjectGraphType<Step>
{
    public StepType()
    {
        Name = "Step";

        Field(s => s.Id, type: typeof(IdGraphType)).Description("The ID of the Step");
        Field(s => s.StepId, type: typeof(IntGraphType)).Description("The SQL ID of the Step");
        Field(s => s.Direction, type: typeof(StringGraphType)).Description("The Name of the Step");
        Field(s => s.SortOrder, type: typeof(IntGraphType)).Description("The Order of the Step");
    }
}
