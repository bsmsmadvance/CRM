SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_GetUnitCountFromProjectIDAndUnitStatus](
    @ProjectID NVARCHAR(50),
	@UnitStatusKey NVARCHAR(10)
)RETURNS INT
BEGIN

    DECLARE @UnitCountTable TABLE(UnitCount INT);

    INSERT INTO @UnitCountTable 
    SELECT COUNT(*) AS UnitCount
    FROM [PRJ].[Project] P WITH (NOLOCK)
            LEFT OUTER JOIN [PRJ].[Unit] U WITH (NOLOCK) ON U.ProjectID = P.ID
            LEFT OUTER JOIN [MST].[MasterCenter] MCUS WITH (NOLOCK) ON MCUS.ID = U.UnitStatusMasterCenterID
    WHERE P.ID = @ProjectID AND MCUS.[Key] = @UnitStatusKey

    DECLARE @result INT;
    SET @result = ISNULL((SELECT UnitCount FROM @UnitCountTable),0)
	RETURN @result;
END


GO
