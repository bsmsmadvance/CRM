SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--SELECT PhoneType, [Key], IsActive, CountryCode, PhoneNumber, PhoneNumberExt, IsMain FROM [dbo].[fn_GetContactPhone] ('992316f1-db0c-4654-847d-444553316996')

ALTER FUNCTION [dbo].[fn_GetContactPhone]
(
	@ContactID NVARCHAR(40),
    @MasterCenterGroupKey NVARCHAR(5)
)
RETURNS @ContactPhone TABLE (ContactName NVARCHAR(50), PhoneType NVARCHAR(50), [Key] NVARCHAR(50), IsActive NVARCHAR(10), CountryCode NVARCHAR(10), PhoneNumber NVARCHAR(20), PhoneNumberExt NVARCHAR(20), IsMain NVARCHAR(10))

AS
BEGIN

    INSERT INTO @ContactPhone 
    SELECT  'ContactName' = C.FirstNameTH + ' ' + C.LastNameTH
            ,'PhoneType' = MC.Name
            , MC.[Key]
            , MC.IsActive
            , CP.CountryCode
            , CP.PhoneNumber
            , CP.PhoneNumberExt
            , CP.IsMain
    FROM [CTM].[ContactPhone] CP
    LEFT OUTER JOIN [CTM].[Contact] C on C.ID = CP.ContactID
    LEFT OUTER JOIN [MST].[MasterCenter] MC on MC.ID = CP.PhoneTypeMasterCenterID
    WHERE CP.ContactID = @ContactID AND MC.[Key] = @MasterCenterGroupKey AND MC.MasterCenterGroupKey = 'PhoneType'

RETURN
END


GO
