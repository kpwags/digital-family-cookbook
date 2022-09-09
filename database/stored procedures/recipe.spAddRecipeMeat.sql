DROP PROCEDURE IF EXISTS [recipe].[spAddRecipeMeat];
GO

CREATE PROCEDURE [recipe].[spAddRecipeMeat]
    @RecipeId INT,
    @MeatId INT
/*--------------
Adds a recipe meat record
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO [recipe].[RecipeMeat] ([RecipeId], [MeatId], [Id])
    VALUES (@RecipeId, @MeatId, NEWID());
END;
GO