DROP PROCEDURE IF EXISTS [recipe].[spAddRecipeCategory];
GO

CREATE PROCEDURE [recipe].[spAddRecipeCategory]
    @RecipeId INT,
    @CategoryId INT
/*--------------
Adds a recipe category record
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [recipe].[RecipeCategory] ([RecipeId], [CategoryId], [Id])
    VALUES (@RecipeId, @CategoryId, NEWID());
END;
GO