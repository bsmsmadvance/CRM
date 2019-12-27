
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_013
    @ContractNumber NVARCHAR(4000)
AS
BEGIN
	SELECT '' as LetterHeadNumber, '' as NotificationDate2, '' as NotificationDate, '' as Customer, '' as Address1, '' as Address2, '' as CompanyID, '' as CompanyName, '' as ProjetcName, '' as UnitNumber, '' as ContractNumber, '' as ContractDate,
	'' as Period, '' as DueDate, '' as PayableAmount, '' as Producttype from ReportTemplate
END
GO

exec AP2SP_PF_AG_013