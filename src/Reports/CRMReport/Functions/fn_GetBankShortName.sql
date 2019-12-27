SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- select [dbo].[fn_GetBankShortName](014)

CREATE FUNCTION [dbo].[fn_GetBankShortName](
      @BankID nvarchar(20)
)
RETURNS nvarchar(255)
AS
BEGIN
      RETURN ISNULL((SELECT AdBankName FROM [ICON_EntForms_Bank] WHERE BankID=@BankID), @BankID);
END






GO
