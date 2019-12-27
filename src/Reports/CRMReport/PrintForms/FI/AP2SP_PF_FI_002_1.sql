
CREATE OR ALTER PROCEDURE AP2SP_PF_FI_002_1
	@ReceivedID nvarchar(4000), --เลขที่ใบเสร็จรับเงิน
	@RCReferent nvarchar(4000)
AS
BEGIN
	SELECT '' as PaymentType, '' as ReceivedID, '' as RCReferent, '' as Details, '' as Amount from ReportTemplate
END
GO

exec AP2SP_PF_FI_002_1