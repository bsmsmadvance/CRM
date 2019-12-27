
CREATE OR ALTER PROCEDURE AP2SP_RP_TR_001
--	@ProductID nvarchar(50),
	@Projects	nvarchar(4000),
	@UnitNumber nvarchar(MAX),
	@DateStart datetime,
	@DateEnd   datetime,
    @StatusAG   nvarchar(20),
	@LoanStatus nvarchar(2),
	@LoanStatus1 nvarchar(2),
	@BankOnly nvarchar(20),
	@UserName nvarchar(150),
	@DateStart2 datetime,
	@DateEnd2   datetime,
	@DateStart3 datetime,
	@DateEnd3   datetime,
	@DateStart4 datetime,
	@DateEnd4   datetime
AS
BEGIN
	SELECT '' as Project, '' as UnitNumber, '' as AddressNumber, '' as ContractNumber, '' as CustomerName, '' as AdBankName, '' as LoanDate, '' as BookingDate, '' as ContractDate, '' as CheckDocCompleteDate, '' as TransferDate, '' as TransferRealDate,
	'' as CustomerStatus, '' as TimeLine, '' as LoanStatus, '' as LoanDateAccepted, '' as LoanAccepted, '' as InsuranceAmount, '' as LoanAcceptedAP, '' as BankName, '' as Reason, '' as ReasonNoSelect, '' as Remark, '' as CreateByName from ReportTemplate
END
GO

exec AP2SP_RP_TR_001