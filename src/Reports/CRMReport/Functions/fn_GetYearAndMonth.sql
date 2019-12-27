SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--DECLARE @ReturnValue VARCHAR(50)
--EXEC @ReturnValue = [dbo].[fn_GetYearAndMonth] 25
--SELECT @ReturnValue

CREATE FUNCTION [dbo].[fn_GetYearAndMonth](
	@Months nvarchar(50)
)RETURNS varchar(150)
BEGIN

    DECLARE @result varchar(50);
    DECLARE @Year nvarchar(2);
    DECLARE @Month nvarchar(2);
    DECLARE @MonthText varchar(20);

    SET @Year = @Months / 12;
    SET @Month = @Months % 12;
    SET @MonthText = CASE WHEN @Month != 0 THEN @Month + N' เดือน' ELSE '' END;
    SET @result = CASE WHEN @Months < 12 THEN @Months + N' เดือน' ELSE @Year + N' ปี ' + @MonthText END;

	RETURN @result;
END


GO
