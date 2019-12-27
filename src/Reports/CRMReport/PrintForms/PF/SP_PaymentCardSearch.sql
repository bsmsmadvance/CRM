
--

CREATE OR ALTER PROCEDURE SP_PAYMENTCARDSEARCH
	@ContractNumber	nvarchar(Max) = ''
AS
BEGIN
	SELECT '' as Sample from ReportTemplate
END
GO

exec SP_PAYMENTCARDSEARCH