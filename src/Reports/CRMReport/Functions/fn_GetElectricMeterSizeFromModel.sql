SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetElectricMeterSizeFromModel](
	@ModelID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT TOP(1) ElectricMeterSize
			FROM [PRJ].[WaterElectricMeterPrice]
			WHERE ModelID = @ModelID ORDER BY Version desc
		)	
END


GO
