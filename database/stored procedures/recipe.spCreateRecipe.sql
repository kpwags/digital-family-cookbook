DROP PROCEDURE IF EXISTS [recipe].[spCreateRecipe];
GO

CREATE PROCEDURE [recipe].[spCreateRecipe]
    @Name NVARCHAR(255),
    @Description NVARCHAR(2000),
    @IsPublic BIT,
    @Servings INT,
    @Source NVARCHAR(255) = NULL,
    @SourceUrl NVARCHAR(500) = NULL,
    @Time INT = NULL,
    @ActiveTime INT = NULL,
    @ImageUrl NVARCHAR(500) = '',
    @ImageUrlLarge NVARCHAR(500) = '',
    @Calories DECIMAL(10, 2) = NULL,
    @Carbohydrates DECIMAL(10, 2) = NULL,
    @Sugar DECIMAL(10, 2) = NULL,
    @Fat DECIMAL(10, 2) = NULL,
    @Protein DECIMAL(10, 2) = NULL,
    @Fiber DECIMAL(10, 2) = NULL,
    @Cholesterol DECIMAL(10, 2) = NULL,
    @UserAccountId NVARCHAR(450),
    @Id NVARCHAR(36)
/*--------------
Creates a recipe
--------------*/
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @RecipeId INT;

    INSERT INTO [recipe].[Recipe] 
    (
        [Name],
        [Description],
        [IsPublic],
        [Servings],
        [Source],
        [SourceUrl],
        [Time],
        [ActiveTime],
        [ImageUrl],
        [ImageUrlLarge],
        [Calories],
        [Carbohydrates],
        [Sugar],
        [Fat],
        [Protein],
        [Fiber],
        [Cholesterol],
        [UserAccountId],
        [Id],
        [DateCreated],
        [DateUpdated]
    ) VALUES (
        @Name,
        @Description,
        @IsPublic,
        @Servings,
        @Source,
        @SourceUrl,
        @Time,
        @ActiveTime,
        @ImageUrl,
        @ImageUrlLarge,
        @Calories,
        @Carbohydrates,
        @Sugar,
        @Fat,
        @Protein,
        @Fiber,
        @Cholesterol,
        @UserAccountId,
        @Id,
        GETDATE(),
        GETDATE()
    );

    SELECT @RecipeId = SCOPE_IDENTITY();
    
    SELECT [RecipeId] = @RecipeId;
END;
GO