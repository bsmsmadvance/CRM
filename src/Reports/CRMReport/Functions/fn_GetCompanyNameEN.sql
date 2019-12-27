SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT [dbo].[fn_GetCompanyNameEN]('F','20130410')
--SELECT [dbo].[fn_GetCompanyNameEN]('F','20130510')
--SELECT [dbo].[fn_GetCompanyNameEN]('J','20130510')
CREATE FUNCTION [dbo].[fn_GetCompanyNameEN](
	@CompanyID nvarchar(50),
	@OperateDate datetime
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT	CASE WHEN dbo.fn_ClearTime(@OperateDate) < '20130510' 
				THEN ISNULL(NameENOld,NameEN) 
				ELSE ISNULL(NameEN,'') END					
			FROM MST.Company
			WHERE ID = @CompanyID
		)	
END




GO
