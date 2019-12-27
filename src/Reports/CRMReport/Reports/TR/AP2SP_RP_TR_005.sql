
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_005
--	@ProductID nvarchar(50),
	@Projects	nvarchar(4000),
	@UnitNumber nvarchar(MAX),
	@DateStart datetime,
	@DateEnd datetime,
	@LoanStatus nvarchar(2),
	@BankOnly nvarchar(20),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ItemID, '' as Project, '' as UnitNumber, '' as AddressNumber, '' as CustomerName, '' as Telephone, '' as SellingPrice, '' as AgreementArea, 
	'' as TransferArea, '' as CheckDocCompleteDate, '' as CustomerStatus, '' as TransferDate, 
	'' as TransferStatus, '' as LoanStatus, '' as BankName, '' as LoanDate, '' as LoanDateAccepted, '' as LoanStatus, '' as Loan, '' as LoanAccepted, 
	'' as InsuranceAmount, '' as LoanAcceptedAP, '' as IsSelect, '' as Reason, '' as ReasonNoSelect, '' as CreateByName from ReportTemplate
END
GO

exec AP2SP_RP_TR_005