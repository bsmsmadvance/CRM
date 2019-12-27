SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION[dbo].[fn_ClearTime]
(
	@Date datetime
)
RETURNS datetime
BEGIN
	--RETURN dbo.fn_ConvToDate(DAY(@Date), MONTH(@Date), YEAR(@Date));
	RETURN CONVERT(DATE,@Date);
END

GO
