
CREATE OR ALTER PROCEDURE AP2SP_RP_AR_007_2
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
	@DateStart Datetime,
	@DateEnd Datetime,
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyName, '' as ProjectName, '' as ProductID, '' as UnitNumber, '' as TransferDate, '' as CustomerName, '' as TitleDeedNumber, '' as LandSize, '' as TotalSellingPrice, '' as PromotionAndOther, '' as NetSalePrice,
	'' as BookingAmount, '' as ContractAmount, '' as TransferAmount, '' as Total, '' as TotalTransfer, '' as TotalNotTransfer, '' as PayableAmount, '' as DueTotalAmount from ReportTemplate
END
GO

exec AP2SP_RP_AR_007_2