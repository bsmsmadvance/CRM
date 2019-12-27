SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetCompanyName](
	@CompanyID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT NameTH
			FROM MST.Company
			WHERE ID = @CompanyID
		)	
END



GO
