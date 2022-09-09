DROP PROCEDURE IF EXISTS [recipe].[spGetMeats];
GO

CREATE PROCEDURE [recipe].[spGetMeats]

/*--------------
Retrieves the meats
--------------*/
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT
          [MeatId]
        , [Name]
    FROM [recipe].[Meat]
    ORDER BY [MeatId];
END;
GO