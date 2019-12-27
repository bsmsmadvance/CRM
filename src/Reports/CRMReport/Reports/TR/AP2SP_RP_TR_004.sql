
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_004
--	@ProductID nvarchar(50),
	@Projects	nvarchar(4000),
	@UnitNumber nvarchar(MAX),
	@DateStart datetime,
	@DateEnd   datetime,
	@LoanStatus nvarchar(2),
	@BankOnly nvarchar(20),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as ItemID, '' as Project, '' as UnitNumber, '' as AddressNumber, '' as CustomerName, '' as AdBankName, '' as LoanDate, '' as CheckDocCompleteDate, 
	'' as CustomerStatus, '' as TimeLine, '' as LoanStatus, '' as LoanDateAccepted,
	'' as LoanAccepted, '' as InsuranceAmount, '' as LoanAcceptedAP, '' as Reason, '' as ReasonNoSelect, '' as Remark, '' as CreateByName from ReportTemplate
END
GO

exec AP2SP_RP_TR_004