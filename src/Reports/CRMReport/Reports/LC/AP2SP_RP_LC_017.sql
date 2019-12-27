
CREATE OR ALTER PROCEDURE AP2SP_RP_LC_017

	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime,
	@DateEnd   datetime,
	@Username nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyNameThai, '' as ProductID, '' as Project, '' as UnitNumber, '' as AddressNumber, '' as CustomerName, '' as Mobile, 
	'' as TransferDateApprove, '' as Address1 from ReportTemplate
END
GO

exec AP2SP_RP_LC_017