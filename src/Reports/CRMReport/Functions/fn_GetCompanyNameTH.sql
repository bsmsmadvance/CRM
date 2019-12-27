SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT [dbo].[fn_GetCompanyNameTH]('F','20130410')
--SELECT [dbo].[fn_GetCompanyNameTH]('F','20130510')
--SELECT [dbo].[fn_GetCompanyNameTH]('J','20130510')
ALTER FUNCTION [dbo].[fn_GetCompanyNameTH](
	@CompanyID nvarchar(50),
	@OperateDate datetime
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT	CASE WHEN dbo.fn_ClearTime(@OperateDate) < '20130510' 
				THEN ISNULL(NameTHOld,NameTH) 
				ELSE ISNULL(NameTH,'') END					
			FROM MST.Company
			WHERE ID = @CompanyID
		)	
END



GO
