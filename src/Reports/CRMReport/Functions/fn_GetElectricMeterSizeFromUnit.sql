SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetElectricMeterSizeFromUnit](
	@ElectricMeterPriceID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT ElectricMeterSize
			FROM [PRJ].[WaterElectricMeterPrice]
			WHERE ID = @ElectricMeterPriceID
		)	
END

GO
