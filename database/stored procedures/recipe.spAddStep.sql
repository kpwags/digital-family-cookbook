DROP PROCEDURE IF EXISTS [recipe].[spAddStep];
GO

CREATE PROCEDURE [recipe].[spAddStep]
    @RecipeId INT,
    @Direction NVARCHAR(MAX),
    @SortOrder INT,
    @Id NVARCHAR(36)
/*--------------
Adds a step to the given recipe
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [recipe].[Step]
    (
        [RecipeId],
        [Direction],
        [SortOrder],
        [Id],
        [DateCreated],
        [DateUpdated]
    )
    VALUES
    (
        @RecipeId,
        @Direction,
        @SortOrder,
        @Id,
        GETDATE(),
        GETDATE()
    );
END;
GO