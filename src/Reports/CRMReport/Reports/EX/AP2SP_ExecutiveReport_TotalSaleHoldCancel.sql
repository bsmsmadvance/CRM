
CREATE OR ALTER PROCEDURE RPT.ExecutiveReport_TotalSaleHoldCancel
	@Projects varchar(500)='',
	@DateStart DateTime='18000101',
	@DateEnd DateTime='70000101',
	@StatusProject varchar(50)='',
	@UserName nvarchar(150)=''
AS
BEGIN
	SELECT '' as ProductID, '' as Project, '' as BU , '' as StartSale, '' as TotalUnit, '' as TotalPrice, '' as EmptyUnit, '' as EmptyPrice, '' as BookingTotalPrice, '' as BookingTotalUnit, '' as CancelTotalPrice, '' as CancelTotalUnit, '' as ContractPrice,
	'' as TransferDiscount, '' as AreaPrice, '' as ExtraDiscount, '' as TransferTotalPrice, '' as TransferTotalUnit, '' as PTDBookingTotalPrice, '' as PTDBookingTotalUnit, '' as PTDTransferTotalPrice, '' as PTDTransferTotalUnit, '' as HCUnit, '' as HCPrice from ReportTemplate
END
GO

exec RPT.ExecutiveReport_TotalSaleHoldCancel