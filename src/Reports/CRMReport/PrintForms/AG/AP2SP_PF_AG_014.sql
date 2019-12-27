
CREATE OR ALTER PROCEDURE AP2SP_PF_AG_014
    @ContractNumber NVARCHAR(4000)
AS
BEGIN
	SELECT '' as LetterHeadNumber, '' as NotificationDate2, '' as NotificationDate, '' as Customer, '' as Address1, '' as Address2, '' as ContractDate, '' as Notification_1,
	'' as ProjectName, '' as UnitNumber, '' as CountPeriod, '' as TotalMoneyOfPeriod, '' as BathText, '' as Producttype, '' as CompanyID, '' as CompanyNameThai, '' as ContractNumber,
	'' as Notification_2 from ReportTemplate
END
GO

exec AP2SP_PF_AG_014