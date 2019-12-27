
CREATE OR ALTER PROCEDURE AP2SP_RP_FI_003
    @CompanyID  nvarchar(20),
    @ProductID  nvarchar(20),
	@DateStart	datetime,
	@DateEnd	datetime,
    @Discount   nvarchar(10),
	@UserName	nvarchar(50) 
AS
BEGIN
	SELECT '' as CompanyNameThai, '' as Project, '' as CreateDate, '' as CompanyID, '' as ProductID, '' as SBUID, '' as TypeOfRealEstate, '' as UnitNumber, '' as ContractNumber, '' as ContractDate, '' as Customer, '' as Discount,
	'' as DiscountText, '' as RecordBy, '' as CreateBy, '' as SellingPrice from ReportTemplate
END
GO

exec AP2SP_RP_FI_003