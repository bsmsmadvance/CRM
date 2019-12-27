
CREATE OR ALTER PROCEDURE AP2SP_RP_FI_018
	@ProductID nvarchar(50),
	@UnitNumber nvarchar(50),
	@DateStart datetime = '',
	@DateEnd   datetime = '',
	@DateStart2 datetime = '',
	@DateEnd2   datetime = '',
	@AccountStatus nvarchar(2),
    @UserName nvarchar(150)
AS
BEGIN
	SELECT '' as CompanyID, '' as CompanyName, '' as RddBatchID, '' as FileName, '' as BankAccountID, '' as BankAccount, '' as AdBankName, '' as BookTypeName, 
	'' as AccountName, '' as ProductID, '' as ProjectName, '' as SBU, '' as UnitNumber,
	'' as RAccount, '' as Period, '' as PeriodDate, '' as ContractNumber, '' as CustomerName, '' as RAmount, '' as PayInNO, '' as IsPass, '' as TransCode,
	'' as PassORNoPass, '' as TowerID from ReportTemplate
END
GO

exec AP2SP_RP_FI_018