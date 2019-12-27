
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_008
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart  datetime,
	@DateEnd  datetime,
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyName, '' as Project, '' as TypeOfRealEstate, '' as CompanyNameThai, '' as UnitNumber, '' as ContractNumber, '' as SellingPrice, '' as CustomerName, '' as PayableAmount,
	'' as PercentPayable, '' as Amount, '' as PercentAmount, '' as RemainAmount, '' as OperateName, '' as Area, '' as TitleDeedNumber, '' as AddressNumber from ReportTemplate
END
GO

exec AP2SP_RP_AR_008