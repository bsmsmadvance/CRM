SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetWaterMeterSizeFromUnit](
	@WaterMeterPriceID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT WaterMeterSize
			FROM [PRJ].[WaterElectricMeterPrice]
			WHERE ID = @WaterMeterPriceID
		)	
END



GO
