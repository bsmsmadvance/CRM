SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetNormalInstallmentCount](
    @TotalInstallment INT,
	@SpecialInstallment INT
)RETURNS INT
BEGIN

    DECLARE @result INT;
    SET @result = @TotalInstallment + @SpecialInstallment
	RETURN @result;
END




GO
