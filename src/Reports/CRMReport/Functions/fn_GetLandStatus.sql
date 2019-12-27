SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetLandStatus]

(
	@Status nvarchar(20)
)

RETURNS nvarchar(Max)

BEGIN

	RETURN
		(
			SELECT Description
			FROM ICON_EntForms_ExtCod
			WHERE Ref = @Status 
		    AND GType = 'LandStatus'
		)
END

GO
