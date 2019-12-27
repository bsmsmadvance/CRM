
CREATE OR ALTER PROCEDURE AP2SP_RP_LC_001

	@CompanyID nvarchar(50) = '',
	@ProductID nvarchar(50)= '',
	@UnitNumber nvarchar(50) = '',
	@UserName nvarchar(150) = '',
	@DateStart datetime,
	@DateEnd   datetime,
	@DateStart2 datetime,
	@DateEnd2   datetime
AS
BEGIN
	SELECT '' as CompanyNameThai, '' as ProjectName, '' as ProductID, '' as UnitNumber, '' as AddressNumber, '' as TitledeedNumber, '' as Area, 
	'' as TransferNumber, '' as DateOld, '' as DateNew, '' as BankBranch from ReportTemplate
END
GO

exec AP2SP_RP_LC_001