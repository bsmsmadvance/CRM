
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_003_2
    @BookingNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as nOrder, '' as BookingNumber, '' as PromotionDescription, '' as ID, '' as Flag, '' as AmountDiscount from ReportTemplate
END
GO

exec AP2SP_PF_AG_003_2