
CREATE OR ALTER PROCEDURE ZCM_SP_Report_CommissionByProjectTransfer_H
	@HomeType nvarchar(20),
	@ProductID nvarchar(20), 
	@Month int,
	@Year int,
	@UserName nvarchar(250),
	@ProjectGroup nvarchar(5),
	@ProjectType2 nvarchar(5)
AS
BEGIN
	SELECT '' as BUID, '' as ProductID, '' as ProductName, '' as UnitNumber, '' as HCSName, '' as LCHelperID, '' as LCHelperName, '' as LCCID, '' as LCCName, 
	'' as CustomerName, '' as BookingDate, '' as ContractDate, '' as ApproveDate, '' as SignContractApproveDate,
	'' as TransferDateApprove, '' as NetSalePrice, '' as CommissionRatePercentTransfer, '' as SaleCommissionTransPaid, '' as LCCCommissionTransPaid, 
	'' as TotalCommissionPaid from ReportTemplate
END
GO

exec ZCM_SP_Report_CommissionByProjectTransfer_H