DROP PROCEDURE IF EXISTS [recipe].[spAddIngredient];
GO

CREATE PROCEDURE [recipe].[spAddIngredient]
    @RecipeId INT,
    @Name NVARCHAR(255),
    @SortOrder INT,
    @Id NVARCHAR(36)
/*--------------
Adds an ingredient to the given recipe
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [recipe].[Ingredient]
    (
        [RecipeId],
        [Name],
        [SortOrder],
        [Id],
        [DateCreated],
        [DateUpdated]
    )
    VALUES
    (
        @RecipeId,
        @Name,
        @SortOrder,
        @Id,
        GETDATE(),
        GETDATE()
    );
END;
GO