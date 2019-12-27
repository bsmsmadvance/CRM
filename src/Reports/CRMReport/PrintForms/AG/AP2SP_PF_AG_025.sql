
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_025
	@ContractNumber nvarchar(20),
	@HistoryID nvarchar(20),
	@OperateType nvarchar(20)
AS
BEGIN
	SELECT '' as BankAccount, '' as CompanyNameEng, '' as UnitNumber, '' as ProjectNameEng, '' as Tel, '' as TowerID, '' as CustomerName from ReportTemplate
END
GO

exec AP2SP_PF_AG_025