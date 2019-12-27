SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetProjectName](
	@ProjectID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT ProjectNameTH
			FROM [PRJ].[Project]
			WHERE ProjectNo = @ProjectID
		)
END




GO
