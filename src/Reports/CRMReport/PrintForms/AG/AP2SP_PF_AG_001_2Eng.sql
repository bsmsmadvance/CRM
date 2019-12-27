
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_001_2Eng
    @ContractNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as Period, '' as AMT, '' as AMTTtext, '' as DueDate from ReportTemplate
END
GO

exec AP2SP_PF_AG_001_2Eng