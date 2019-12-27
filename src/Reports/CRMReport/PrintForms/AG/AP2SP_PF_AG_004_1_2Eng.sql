
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_004_1_2Eng
	@ContractNumber nvarchar(50)
AS
BEGIN
	SELECT '' as PayableAmount, '' as PayableAmountText, '' as DueDate, '' as TransferDiscount, '' as TransferDiscountText, '' as BookingDate, '' as ContractVersion from ReportTemplate
END
GO

exec AP2SP_PF_AG_004_1_2Eng