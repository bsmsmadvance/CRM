SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetWaterMeterSizeFromModel](
	@ModelID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT TOP(1) WaterMeterSize
			FROM [PRJ].[WaterElectricMeterPrice]
			WHERE ModelID = @ModelID ORDER BY Version desc
		)	
END

GO
