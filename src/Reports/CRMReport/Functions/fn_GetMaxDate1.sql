SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




--SELECT DateAdd(minute,-1,DateAdd(day,1,'2010-02-21'))
--SELECT DateAdd(minute,-1,DateAdd(day,0,getdate()))
ALTER FUNCTION [dbo].[fn_GetMaxDate1](
	@Date Datetime
)RETURNS Datetime
BEGIN
	RETURN
		(
			SELECT DateAdd(second,-1,DateAdd(day,0,@Date))
		)
END







GO
