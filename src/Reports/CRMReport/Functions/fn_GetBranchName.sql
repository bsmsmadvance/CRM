SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--select [dbo].[fn_GetBranchName]('331','014')
CREATE FUNCTION [dbo].[fn_GetBranchName](
	@BranchID nvarchar(50),
	@BankID nvarchar(50)
)RETURNS nvarchar(150)
BEGIN
	RETURN
		(
			SELECT BranchName
			FROM ICON_EntForms_BankBranch
			WHERE BranchID = @BranchID 
                  AND BankID = @BankID 
		)
END




GO
