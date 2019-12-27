
CREATE OR ALTER PROCEDURE AP2SP_NRP_TR_002
	@CompanyID nvarchar(50),
	@ProductID nvarchar(50),
    @Month nvarchar(10),
    @Year nvarchar(10),
	@UserName nvarchar(150)
AS
BEGIN
	SELECT '' as TransferNumber, '' as ContractNumber, '' as CompanyName, '' as ProjectName, '' as TransferDate, '' as UnitNumber, '' as AddressNumber, '' as CustomerName, '' as Price, '' as TransferPayment, '' as LoanAccepted,
	'' as PercentLoan, '' as InsuranceAmount, '' as Cash, '' as BankNyProject, '' as BranchByProject, '' as Bank, '' as Branch, '' as Remark from ReportTemplate
END
GO

exec AP2SP_NRP_TR_002