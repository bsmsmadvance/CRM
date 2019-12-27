
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_001_1Eng
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as PayableAmount, '' as PayableAmountText, '' as DueDate from ReportTemplate
END
GO

exec AP2SP_PF_AG_001_1Eng