
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_005_1_2
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as PayableAmount, '' as PayableAmountText, '' as DueDate, '' as TransferDiscount, '' as TrasnferDiscountText, '' as BookingDate, '' as ContractVersion from ReportTemplate
END
GO

exec AP2SP_PF_AG_005_1_2