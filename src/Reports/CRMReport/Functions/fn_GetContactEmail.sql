SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT ContactName, Email, IsMain FROM [dbo].[fn_GetContactEmail] ('992316f1-db0c-4654-847d-444553316996')

CREATE FUNCTION [dbo].[fn_GetContactEmail]
(
	@ContactID NVARCHAR(40)
)
RETURNS @ContactEmail TABLE (ContactName NVARCHAR(50), Email NVARCHAR(50), IsMain NVARCHAR(10))

AS
BEGIN

    INSERT INTO @ContactEmail 
    SELECT  'ContactName' = C.FirstNameTH + ' ' + C.LastNameTH
            , CE.Email
            , CE.IsMain
    FROM [CTM].[ContactEmail] CE
    LEFT OUTER JOIN [CTM].[Contact] C on C.ID = CE.ContactID
    WHERE CE.ContactID = @ContactID AND CE.IsMain = 1

RETURN
END


GO
