
CREATE OR ALTER PROCEDURE AP2SP_PF_FI_002_2
	@ReceivedID nvarchar(4000) --เลขที่ใบเสร็จรับเงิน
AS
BEGIN
	SELECT '' as ReceivedID, '' as AmountInReceived, '' as MethodName, '' as ChequeBankName, '' as ChequeBranchName, '' as ChequeNumber, '' as ChequeDueDate, '' as ChequeAmount,
	'' as CreditBankName, '' as CreateType, '' as CreditAccount, '' as ExpireDate, '' as AccountName, '' as Method, '' as TmpCash, '' as TmpChequeCash, '' as TmpCredit,
	'' as TmpCheque1, '' as TmpCheque2, '' as TmpTF, '' as TmpPayIn, '' as TmpDDebit, '' as TmpDCredit, '' as CashAmount, '' as TransferAmount, '' as CreditAmount from ReportTemplate
END
GO

exec AP2SP_PF_FI_002_2