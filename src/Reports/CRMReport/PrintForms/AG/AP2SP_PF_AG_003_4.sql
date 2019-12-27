
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_003_4
    @BookingNumber  nvarchar(50)
AS
BEGIN
	SELECT '' as ReferentID, '' as Method, '' as Cash, '' as Cheque, '' as Credit, '' as Amount, '' as Number, '' as CreditType, '' as BankName, '' as BranchName, '' as RDate, '' as DueDate, '' as HeaderDesc from ReportTemplate
END
GO

exec AP2SP_PF_AG_003_4