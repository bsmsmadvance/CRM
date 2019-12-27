
CREATE OR ALTER PROCEDURE CRM_SP_PRINTOUT_CustomerAddress
    @ContactID  nvarchar(20)
AS
BEGIN
	SELECT '' as BookDate, '' as ProjectID, '' as UnitNumber, '' as ProjectName, '' as refCustomerID, '' as FirstName, '' as LastName, '' as BirthDate, '' as AddressNo,
	'' as Building, '' as Moo, '' as Soi, '' as Road, '' as SubDistrict, '' as District, '' as Province, '' as Postcode, '' as Country, '' as Mobile, '' as Phone, '' as Email,
	'' as AddressNo2, '' as Building2, '' as Moo2, '' as Soi2, '' as Road2, '' as SubDistrict2, '' as District2, '' as Province2, '' as Postcode2, '' as Country2, '' as Mobile2,
	'' as Phone2 from ReportTemplate
END
GO

exec CRM_SP_PRINTOUT_CustomerAddress