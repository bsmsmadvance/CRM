
CREATE OR ALTER PROCEDURE AP2SP_PF_FI_002

	@ReceivedID nvarchar(4000), --เลขที่ใบเสร็จรับเงิน
	@UserID nvarchar(50)

AS
BEGIN
	SELECT '' as CompanyNameThai, '' as CompanyNameEng, '' as AddressThai, '' as AddressEng, '' as Tel, '' as CompanyID, '' as PersonPayForTax, '' as ReceivedID, '' as ReceiveDate,
	'' as PrintingID, '' as RCReferent, '' as ProductID, '' as UnitNumber, '' as RDate, '' as AmountInReceived, '' as AmountText, '' as AmountTextEng, '' as CustomerName,
	'' as Address1, '' as Address2, '' as Address3, '' as CusTel from ReportTemplate
END
GO

exec AP2SP_PF_FI_002