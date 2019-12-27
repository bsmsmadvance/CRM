
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_009
	@ContractNumber nvarchar(50),
	@HistoryID nvarchar(50)
AS
BEGIN
	SELECT '' as ProjectName, '' as OperateDate, '' as ContractDate, '' as CompanyNameThai, '' as Address, '' as Road, '' as Soi, '' as SubDistrict, '' as District, '' as Province, '' as ContractName, '' as Age, '' as AddressNumber,
	'' as Moo, '' as ContractNumber, '' as UnitNumber, '' as OldDown, '' as NewDOwn, '' as StartPeriod, '' as StartPeriodDate, '' as PeriodDate, '' as OldDate, '' as NewDate, '' as FormType, '' as TransferDate, '' as TransferPayment,
	'' as TransferPaymentText from ReportTemplate
END
GO

exec AP2SP_PF_AG_009