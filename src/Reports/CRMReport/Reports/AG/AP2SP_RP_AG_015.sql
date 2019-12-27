
CREATE OR ALTER PROCEDURE AP2SP_RP_AG_015
	@DateStart	datetime,
	@DateStart2 datetime,
	@UserName	nvarchar(50)
AS
BEGIN
	SELECT '' as ContactID, '' as AgreementName, '' as CustomerStatusName, '' as EmployeeAgreementType, '' as ProductID,
	'' as ProjectName, '' as DueDay, '' as UnitNumber, '' as ContractNumber, '' as PayableAmount, '' as Period, '' as Amount,
	'' as Period1, '' as Amount1, '' as MaxPay, '' as LetterType, '' as Total from ReportTemplate
END
GO

exec AP2SP_RP_AG_015