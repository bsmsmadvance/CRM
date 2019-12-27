
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_015
    @ContractNumber NVARCHAR(4000)
AS
BEGIN
	SELECT '' as LetterHeadNumber, '' as ContractNumber, '' as NotificationDate2, '' as NotificationDate, '' as Customer, '' as Address1, '' as Address2, '' as ContractDate,
	'' as Notification_1, '' as Notification_2, '' as ProjectName, '' as UnitNumber, '' as CompanyID, '' as CompanyName, '' as Installment, '' as Producttype, '' as TotalMoneyOfPeriod,
	'' as BathText from ReportTemplate
END
GO

exec AP2SP_PF_AG_015