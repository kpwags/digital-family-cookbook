namespace DigitalFamilyCookbook.GraphQL.Types;

public class RecipeStepType : ObjectGraphType<RecipeStep>
{
    public RecipeStepType()
    {
        Name = "RecipeStep";

        Field(rs => rs.Id, type: typeof(IdGraphType)).Description("The ID of the RecipeStepId");
        Field(rs => rs.RecipeStepId, type: typeof(IntGraphType)).Description("The SQL ID of the RecipeStepId");
        Field(rs => rs.RecipeId, type: typeof(IntGraphType)).Description("The ID of the recipe");
        Field(rs => rs.Recipe, type: typeof(RecipeType)).Description("The recipe");
        Field(rs => rs.StepId, type: typeof(IntGraphType)).Description("The ID of the step");
        Field(rs => rs.Step, type: typeof(StepType)).Description("The step");
    }
}
