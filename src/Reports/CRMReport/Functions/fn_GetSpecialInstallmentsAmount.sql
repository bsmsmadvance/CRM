SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER FUNCTION [dbo].[fn_GetSpecialInstallmentAmount](
	@SpecialInstallment varchar(50)
)RETURNS money
BEGIN
DECLARE @rtStringVar money
SELECT @rtStringVar = CASE CHARINDEX(',', LTRIM(@SpecialInstallment), 1)
WHEN 0 THEN LTRIM(@SpecialInstallment)
ELSE SUBSTRING(LTRIM(@SpecialInstallment), 1, CHARINDEX(',',LTRIM(@SpecialInstallment), 1) - 1)
END
RETURN @rtStringVar
END


GO
