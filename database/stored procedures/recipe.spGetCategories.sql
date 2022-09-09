DROP PROCEDURE IF EXISTS [recipe].[spGetCategories];
GO

CREATE PROCEDURE [recipe].[spGetCategories]

/*--------------
Gets the available categories
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT
          [CategoryId]
        , [Name]
    FROM [recipe].[Category]
    ORDER BY [CategoryId];
END;
GO